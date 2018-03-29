using ComicOrders.WPF.ViewModels;
using System.Windows;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OrdersView : Window {
        public OrdersView() {
            var data = new OrdersViewModel(true);
            this.Content = data;
            InitializeComponent();
        }
    }
}
