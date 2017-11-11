using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using Dapper.Contrib;
using ComicOrderForm.Models;

namespace ComicOrderForm.Helpers {
    public static class DbUtil {

        public static string DbDirectory { get { return $@"{AppDomain.CurrentDomain.BaseDirectory}\data\"; } }
        public static string DbPath { get { return $@"{DbDirectory}\comicorders.db"; } }
        public static string BaseConnectionString { get { return $@"{DbPath};Version=3;"; } }
        public static string ConnectionString { get { return $"{BaseConnectionString};password={PASSWORD}"; } }
        public const string PASSWORD = "pickles";

        public static bool InitialzieDatabase() {
            SQLiteConnection.CreateFile(DbPath);
            if(!System.IO.File.Exists(DbPath)) {
                System.Windows.MessageBox.Show("Database could not be initialized. Something very wrong has happened. Contact Ian.");
                //System.Windows.Application.Current.Shutdown(-1);
                return false;
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
                return false;
            }
            return true;
        }



        public static dynamic AddOrders(ICollection<CustomerModel> Customers, ICollection<ComicModel> Comics) {
            return null;
        }
    }
}
