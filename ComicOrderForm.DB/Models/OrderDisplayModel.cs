using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ComicOrders.DB.Models {
    public class OrderDisplayModel {
        public OrderModel Order { get; set; }
        public CustomerModel Customer { get; set; }
        public ComicModel Comic { get; set; }

        public OrderDisplayModel(long OrderId) {
            Order = DbUtil.GetById<OrderModel>(OrderId);
            Comic = DbUtil.GetById<ComicModel>(Order.ComicId);
            Customer = DbUtil.GetById<CustomerModel>(Order.CustomerId);
        }

        public OrderDisplayModel(OrderModel Order) {
            this.Order = Order;
            Comic = DbUtil.GetById<ComicModel>(Order.ComicId);
            Customer = DbUtil.GetById<CustomerModel>(Order.CustomerId);
        }

        /// <summary>
        /// Fetches the orders for the given month and year, and converts them to OrderDisplayModels.
        /// </summary>
        /// <param name="OrderMonth">The order month and year.</param>
        /// <returns>List of OrderDisplayModels.</returns>
        public static ICollection<OrderDisplayModel> GetOrders(DateTime OrderMonth) {
            var displayOrders = new List<OrderDisplayModel>();
            using(var cn = new SQLiteConnection(DbUtil.ConnectionString)) {
                var orders = cn.Query<OrderModel>("SELECT * FROM Orders WHERE OrderMonth = @month AND OrderYear = @year", new { month = OrderMonth.Month, year = OrderMonth.Year });
                foreach(var order in orders) {
                    displayOrders.Add(new OrderDisplayModel(order));
                }
            }
            return displayOrders;
        }
    }
}
