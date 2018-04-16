using System.Windows;

namespace ComicOrders.WPF.Views {
    /// <summary>
    /// Interaction logic for ComicsAddEditView.xaml
    /// </summary>
    public partial class ComicsAddEditView : Window {
        public ComicsAddEditView() {
            this.DataContext = new ComicsAddEditView();
            InitializeComponent();
        }

        public ComicsAddEditView(long ComicId) {
            this.DataContext = new ComicsAddEditView(ComicId);
            InitializeComponent();
        }
    }
}
