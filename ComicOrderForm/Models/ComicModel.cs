using Dapper.Contrib.Extensions;
using System;

namespace ComicOrderForm.Models {
    [Table("Comics")]
    public class ComicModel {
        [Key]
        public long Id { get; set; }
        public string DiamondCode { get; set; }
        public string IssueNumber { get; set; }
        public decimal RetailCost { get; set; }
        public bool IsVariant { get; set; }
        public long? NonVariantComicId { get; set; }
        public long? SeriesId { get; set; }
        public DateTime? ReleaseMonth { get; set; }

        public static int TableOrder() {
            return 1;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE Comics(Id PRIMARY KEY,
                                            DiamondCode TEXT,
                                            IssueNumber TEXT,
                                            RetailCost REAL,
                                            IsVariant BOOLEAN,
                                            NonVariantComicId INT,
                                            SeriesId INT,
                                            ReleaseMonth TEXT
                    )";
        }
    }
}
