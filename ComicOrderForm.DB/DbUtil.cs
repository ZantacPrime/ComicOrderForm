using ComicOrders.DB.Models;
using ComicOrders.Lib.Helpers;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public static T GetById<T>(long Id) where T:class {
            using(var cn = new SQLiteConnection(ConnectionString)) {
                return cn.Get<T>(Id);
            }
        }

        //public static string BaseConnectionString { get { return $@"{DbPath};Version=3;"; } }
        //private static string _connectionString;
        private static SQLiteConnectionStringBuilder connBuilder { get;  set; }
        public static string ConnectionString { get {
                if(connBuilder == null)
                    initializeConnectionStringBuilder();
                return connBuilder.ConnectionString; } }
        public const string PASSWORD = "pickles";

        private static void initializeConnectionStringBuilder() {
            connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = DbPath;
#if !DEBUG
            connBuilder.Password = PASSWORD;
#endif
            connBuilder.FailIfMissing = true;
            connBuilder.ReadOnly = false;
            connBuilder.Version = 3;
        }

        public static bool DbExists() {
            return System.IO.File.Exists(DbPath);
        }

        public static bool InitialzieDatabase() {
            System.IO.Directory.CreateDirectory(DbDirectory);
            SQLiteConnection.CreateFile(DbPath);
            if(!DbExists()) {
                throw new Exception("Database could not be initialized.Something very wrong has happened.Contact Ian.");
            }

            try {
                using(var cn = GetDefaultConnection()) {
                    //cn.SetPassword(PASSWORD);
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

        public static IEnumerable<DateTime> GetOrderMonths() {
            var dates = new List<DateTime>();
            using(var cn = GetDefaultConnection()) {
                foreach(var orderMonths in cn.Query<DateTime>("SELECT DISTINCT Date(OrderMonth, '%Y-%m') FROM Orders")) {
                    dates.Add(orderMonths);
                }
            }
            return dates.OrderBy(x => x);
        }

        public static dynamic AddOrders(ICollection<CustomerModel> Customers, ICollection<ComicModel> Comics) {
            return null;
        }

        public static SQLiteConnection GetDefaultConnection() {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
