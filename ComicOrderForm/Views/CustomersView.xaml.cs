using ComicOrders.WPF.ViewModels;
using System.Windows;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : Window {
        public CustomersView() {
            this.DataContext = new CustomersViewModel();
            InitializeComponent();
        }
    }
}
