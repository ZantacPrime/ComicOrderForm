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

        #region Properties
        public static string DbDirectory { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\data\"; } }
        public static string DbPath { get { return $@"{DbDirectory}\comicorders.db"; } }

        private static SQLiteConnectionStringBuilder connBuilder { get; set; }
        public static string ConnectionString {
            get {
                if(connBuilder == null)
                    initializeConnectionStringBuilder();
                return connBuilder.ConnectionString;
            }
        }
        public const string PASSWORD = "pickles";
        #endregion

        public static SQLiteConnection GetDefaultConnection() {
            return new SQLiteConnection(ConnectionString);
        }

        #region Initialization
        /// <summary>
        /// Initializes the connection string builder.
        /// </summary>
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

        /// <summary>
        /// Checks if the db exists.
        /// </summary>
        /// <returns>True if the db file exists. False otherwise.</returns>
        public static bool DbExists() {
            return System.IO.File.Exists(DbPath);
        }

        /// <summary>
        /// Creates the db and schema.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool CreateDatabase() {
            System.IO.Directory.CreateDirectory(DbDirectory);
            SQLiteConnection.CreateFile(DbPath);
            if(!DbExists()) throw new Exception("Database could not be initialized.Something very wrong has happened.Contact Ian.");

            try {
                using(var cn = GetDefaultConnection()) {
#if !DEBUG
                    cn.SetPassword(PASSWORD);
#endif
                    cn.Query(ComicModel.GetTableDefinition());
                    cn.Query(CustomerModel.GetTableDefinition());
                    cn.Query(ComicSeriesModel.GetTableDefinition());
                    cn.Query(OrderModel.GetTableDefinition());
                }
            } catch(Exception ex) {
                Logger.LogError("DB Initialization", ex);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Fetches distinct order month + year combinations.
        /// </summary>
        /// <returns>Collection of datetimes coresponding to the unique month + year combinations.</returns>
        public static IEnumerable<DateTime> GetOrderMonths() {
            var dates = new List<DateTime>();
            using(var cn = GetDefaultConnection()) {
                foreach(var orderMonths in cn.Query<DateTime>("SELECT DISTINCT Date(OrderMonth, '%Y-%m') FROM Orders")) {
                    dates.Add(orderMonths);
                }
            }
            return dates.OrderBy(x => x);
        }

        /// <summary>
        /// Adds orders to the database.
        /// </summary>
        /// <param name="Customers"></param>
        /// <param name="Comics"></param>
        /// <returns></returns>
        public static dynamic AddOrders(ICollection<CustomerModel> Customers, ICollection<ComicModel> Comics) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a db model row by ID.
        /// </summary>
        /// <typeparam name="T">The model being fetched.</typeparam>
        /// <param name="Id">Id of the row to fetch.</param>
        /// <returns>The fetched row.</returns>
        public static T GetById<T>(long Id) where T : class {
            using(var cn = new SQLiteConnection(ConnectionString)) {
                return cn.Get<T>(Id);
            }
        }

        public static IEnumerable<T> GetAll<T>() where T : class {
            using(var cn = GetDefaultConnection()) return cn.GetAll<T>();
        }

        /// <summary>
        /// Updates the given model type in the db.
        /// </summary>
        /// <typeparam name="T">The type of the row being updated.</typeparam>
        /// <param name="Model">The row model being updated.</param>
        /// <returns>True if it updates, false otherwise.</returns>
        public static bool Update<T>(T Model) where T : class {
            using(var cn = GetDefaultConnection()) return cn.Update<T>(Model);

        }

        /// <summary>
        /// Inserts the given model type into the db.
        /// </summary>
        /// <typeparam name="T">The type of the row being inserted.</typeparam>
        /// <param name="Model">The row being inserted.</param>
        /// <returns>The number of rows inserted.</returns>
        public static long Insert<T>(T Model) where T : class {
            using(var cn = GetDefaultConnection()) return cn.Insert<T>(Model);
        }

        public static bool Delete<T>(T Model) where T: class {
            using(var cn = GetDefaultConnection()) return cn.Delete(Model);
        }

        public static bool Delete<T>(List<T> Models) where T: class {
            using(var cn = GetDefaultConnection()) return cn.Delete(Models);
        }
    }
}