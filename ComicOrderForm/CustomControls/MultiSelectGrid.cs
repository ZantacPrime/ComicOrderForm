using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ComicOrders.WPF.CustomControls {
    public class MultiSelectGrid : DataGrid {
        public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MultiSelectGrid), new PropertyMetadata(null));

        public IList SelectedItemsList {
            get => (IList)GetValue(SelectedItemsListProperty);
            set => SetValue(SelectedItemsListProperty, value);
        }

        public MultiSelectGrid() {
            this.SelectionChanged += multiSelectGrid_SelectionChanged;
        }

        void multiSelectGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.SelectedItemsList = this.SelectedItems;
        }
    }
}