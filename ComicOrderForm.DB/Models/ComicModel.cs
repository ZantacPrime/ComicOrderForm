using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ComicOrders.DB.Models {
    [Table("Comics")]
    public class ComicModel {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string DiamondCode { get; set; }
        public string IssueNumber { get; set; }
        public decimal RetailCost { get; set; }
        public bool IsVariant { get; set; }
        public long? NonVariantComicId { get; set; }
        public long? SeriesId { get; set; }
        public DateTime ReleaseDate { get; set; }

        public static int TableOrder() {
            return 1;
        }

        public static string GetTableDefinition() {
            return @"CREATE TABLE Comics(Id INTEGER PRIMARY KEY,
                                            Title TEXT,
                                            DiamondCode TEXT NOT NULL,
                                            IssueNumber TEXT,
                                            RetailCost REAL,
                                            IsVariant BOOLEAN,
                                            NonVariantComicId INT,
                                            SeriesId INT,
                                            ReleaseDate DATE NOT NULL
                    )";
        }

        public override string ToString() {
            return Title ?? DiamondCode;
        }

        public bool HasRequiredFields() {
            return !String.IsNullOrWhiteSpace(Title) &&
                !String.IsNullOrWhiteSpace(DiamondCode) &&
                //!String.IsNullOrWhiteSpace(IssueNumber) &&
                ReleaseDate != default(DateTime);
        }

        //public static List<ComicModel> GetComics(string NameFilter = null, PropertyInfo )
    }
}
