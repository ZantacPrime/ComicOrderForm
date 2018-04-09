using Dapper.Contrib.Extensions;

namespace ComicOrders.DB.Models {
    [Table("ComicSeries")]
    public class ComicSeriesModel {
        [Key]
        public long Id { get; set; }
        public string SeriesName { get; set; }

        public static int TableOrder() {
            return 120;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE ComicSeries (Id INTEGER PRIMARY KEY,
                                                SeriesName TEXT
                                              )";
        }
    }
}
