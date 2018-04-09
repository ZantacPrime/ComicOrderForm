using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.DB.Models {
    [Table("Customers")]
    public class CustomerModel {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLenny { get; set; }

        public static int TableOrder() {
            return 1;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE Customers ( Id INTEGER PRIMARY KEY,
                                                FirstName TEXT,
                                                LastName TEXT,
                                                Email TEXT,
                                                IsLenny BOOLEAN
                                            )";
        }

        public override string ToString() {
            return $"{FirstName} {LastName}";
        }
    }
}
