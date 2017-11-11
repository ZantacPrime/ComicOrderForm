using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrderForm.Models {
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
            return @"CREATE TABLE Customers ( Id PRIMARY KEY,
                                                FirstName TEXT,
                                                LastName TEXT,
                                                Email TEXT,
                                                IsLenny BOOLEAN
                                            )";
        }
    }
}
