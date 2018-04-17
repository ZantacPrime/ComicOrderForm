using ComicOrders.WPF.ViewModels;
using System.Windows;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for ComicsAddEditView.xaml
    /// </summary>
    public partial class ComicsAddEditView : Window {
        public ComicsAddEditView() {
            this.DataContext = new ComicsAddEditViewModel();
            InitializeComponent();
        }

        public ComicsAddEditView(long ComicId) {
            this.DataContext = new ComicsAddEditViewModel(ComicId);
            InitializeComponent();
        }
    }
}
