using ComicOrders.DB.Models;
using ComicOrders.Lib.Helpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.DB {
    public static class DbUtil {

        public static string DbDirectory { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\data\"; } }
        public static string DbPath { get { return $@"{DbDirectory}\comicorders.db"; } }

        internal static T GetById<T>(long orderId) {
            throw new NotImplementedException();
        }

        public static string BaseConnectionString { get { return $@"{DbPath};Version=3;"; } }
        public static string ConnectionString { get { return $"{BaseConnectionString};password={PASSWORD}"; } }
        public const string PASSWORD = "pickles";

        public static bool InitialzieDatabase() {
            SQLiteConnection.CreateFile(DbPath);
            if(!System.IO.File.Exists(DbPath)) {
                throw new Exception("Database could not be initialized.Something very wrong has happened.Contact Ian.");
            }

            try {
                using(var cn = new SQLiteConnection(BaseConnectionString)) {
                    cn.SetPassword(PASSWORD);
                    cn.Query(ComicModel.GetTableDefinition());
                    cn.Query(CustomerModel.GetTableDefinition());
                    cn.Query(ComicSeriesModel.GetTableDefinition());
                    cn.Query(OrderModel.GetTableDefinition());
                }
            } catch(Exception ex) {
                //Log it
                Logger.LogError("DB Initialization", ex);
                return false;
            }
            return true;
        }

        public static IEnumerable<OrderMonthDisplayModel> GetOrderMonths() {
            var dates = new List<OrderMonthDisplayModel>();
            using(var cn = new SQLiteConnection(ConnectionString)) {
                foreach(var values in cn.Query<OrderModel>("SELECT DISTINCT OrderYear, OrderMonth FROM Orders")) {
                    dates.Add(new OrderMonthDisplayModel(values));
                }
            }
            return dates.OrderBy(x => x.OrderMonth);
        }

        public static dynamic AddOrders(ICollection<CustomerModel> Customers, ICollection<ComicModel> Comics) {
            return null;
        }
    }
}
