using ComicOrders.Lib;
using System.Windows;

namespace ComicOrders.WPF.ViewModels {
    public interface IAddEditViewModel {
        bool isEdit { get; }

        RelayCommand<Window> Cancel { get; set; }
        RelayCommand<Window, object> Save {get;set;}
    }
}
