using ComicOrders.WPF.ViewModels;
using System.Windows;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OrdersView : Window {
        public OrdersView() {
            //We'll check for DB now.
            if(!DB.DbUtil.DbExists()) {
                if(!DB.DbUtil.InitialzieDatabase()) {
                    MessageBox.Show("The database could not be created. Contact Ian.");
                    System.Environment.Exit(-1);
                }
            }

            var data = new OrdersViewModel(true);
            this.Content = data;
            InitializeComponent();
        }
    }
}
