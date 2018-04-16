using ComicOrders.WPF.ViewModels;
using System.Windows;
using ComicOrders.WPF;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for ComicsView.xaml
    /// </summary>
    public partial class ComicsView : Window {
        public ComicsView() {
            this.DataContext = new ComicsViewModel();
            InitializeComponent();
        }
    }
}
