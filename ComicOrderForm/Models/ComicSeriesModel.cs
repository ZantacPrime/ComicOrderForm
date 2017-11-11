using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrderForm.Models {
    [Table("ComicSeries")]
    public class ComicSeriesModel {
        [Key]
        public long Id { get; set; }
        public string SeriesName { get; set; }

        public static int TableOrder() {
            return 120;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE ComicSeries (Id PRIMARY KEY,
                                                SeriesName TEXT
                                              )";
        }
    }
}
