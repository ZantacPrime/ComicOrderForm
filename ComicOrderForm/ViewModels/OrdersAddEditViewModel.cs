using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComicOrders.DB.Models;
using ComicOrders.Lib;

namespace ComicOrders.WPF.ViewModels {
    public class OrdersAddEditViewModel : BaseViewModel {
        #region Properties
        private ObservableCollection<DateTime> _orderMonths;
        public ObservableCollection<DateTime> OrderMonths {
            get => _orderMonths;
            set {
                _orderMonths = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _selectedMonth;
        public DateTime? SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComicModel> _availableComics;
        public ObservableCollection<ComicModel> AvailableComics {
            get { return _availableComics; }
            set {
                _availableComics = value;
                OnPropertyChanged();
            }
        }

        private ComicModel _selectedComic;
        public ComicModel SelectedComic {
            get { return _selectedComic; }
            set {
                _selectedComic = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<CustomerModel> _availableCustomers;
        public ObservableCollection<CustomerModel> AvailableCustomers {
            get { return _availableCustomers; }
            set {
                _availableCustomers = value;
                OnPropertyChanged();
            }
        }


        #endregion


        private RelayCommand<object, object> _add;
        public RelayCommand<object, object> Add {
            get => _add;
            set {
                _add = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _remove;
        public RelayCommand<object, object> Remove {
            get => _remove;
            set {
                _remove = value;
                OnPropertyChanged();
            }
        }

        public override string Title => "Order Editor";

    }
}
