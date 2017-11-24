using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrderForm.Models {
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
    }
}
