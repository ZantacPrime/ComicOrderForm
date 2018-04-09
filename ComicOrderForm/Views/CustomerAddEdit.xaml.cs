using ComicOrders.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for CustomerAddEdit.xaml
    /// </summary>
    public partial class CustomerAddEditView : Window {
        public CustomerAddEditView() {
            this.DataContext = new CustomerAddEditViewModel();
            InitializeComponent();
        }

        public CustomerAddEditView(long CustomerId) {
            this.DataContext = new CustomerAddEditViewModel(CustomerId);
            InitializeComponent();
        }

        private void cancel(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
