using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComicOrders.DB.Models;
using ComicOrders.Lib;

namespace ComicOrders.WPF.ViewModels {
    class OrdersAddEditViewModel : BaseViewModel, IAddEditViewModel {
        private OrderModel _order;
        public OrderModel Order {
            get => Order;
            set {
                _order = value;
                OnPropertyChanged();
            }
        }

        public bool isEdit { get; }

        private RelayCommand<Window> _cancel;
        public RelayCommand<Window> Cancel {
            get => _cancel;
            set {
                _cancel = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<Window, object> _save;
        public RelayCommand<Window, object> Save {
            get => _save;
            set {
                _save = value;
                OnPropertyChanged();
            }
        }

        public override string Title {
            get {
                if(isEdit) return "Edit Order";
                return "Add Order";
            }
        }

    }
}
