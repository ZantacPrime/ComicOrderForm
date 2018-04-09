using Dapper.Contrib.Extensions;
using Dapper;
using System;
using System.Collections.Generic;


namespace ComicOrders.DB.Models {
    [Table("Orders")]
    public class OrderModel {
        [Key]
        public long Id { get; set; }
        public long ComicId { get; set; }
        public long CustomerId { get; set; }
        public DateTime OrderMonth { get; set; }

        public static int TableOrder() {
            return int.MaxValue;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE Orders (Id int primary key, 
                                            ComicId int not null, 
                                            CustomerId int not null,
                                            OrderMonth DATE not null,
                                            FOREIGN KEY(ComicId) REFERENCES Comics(Id),
                                            FOREIGN KEY(CustomerId) REFERENCES Customers(Id)
            )";
        }

        //public override string ToString() {
        //    return OrderMonth.ToString("MMMM yyyy");
        //}

        /// <summary>
        /// Gets all orders placed in a given month and year.
        /// </summary>
        /// <param name="OrderMonth">The month and year of the order.</param>
        /// <returns>The orders placed in the given month and year.</returns>
        public static IEnumerable<OrderModel> GetOrdersByOrderMonth(DateTime OrderMonth) {
            using(var cn = DbUtil.GetDefaultConnection()) {
                var first = new DateTime(OrderMonth.Year, OrderMonth.Month, 1);
                var last = first.AddMonths(1).AddDays(-1);
                return cn.Query<OrderModel>("SELECT * FROM Orders WHERE OrderMonth between @first AND @last", new { first, last });
            }
        }
    }
}
