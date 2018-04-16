using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using ComicOrders.WPF.Views;
using ComicOrders.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComicOrders.WPF.ViewModels {
    public class OrdersViewModel : BaseViewModel {

        #region Properties
        public override string Title => "Comic Orders - Special Pickle Edition";

        private ObservableCollection<OrderDisplayModel> _orders;
        public ObservableCollection<OrderDisplayModel> Orders {
            get => _orders;
            set {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private OrderModel _selectedOrder;
        public OrderModel SelectedOrder {
            get => _selectedOrder;
            set {
                _selectedOrder = value;
                OnPropertyChanged();
                populateOrders();
            }
        }

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
        #endregion

        #region Commands
        private RelayCommand<object> _viewCustomers;
        public RelayCommand<object> ViewCustomers {
            get => _viewCustomers;
            set {
                _viewCustomers = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object> _viewComics;
        public RelayCommand<object> ViewComics {
            get => _viewComics;
            set {
                _viewComics = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object> _addOrder;
        public RelayCommand<object> AddOrder {
            get => _addOrder;
            set {
                _addOrder = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _removeSelectedOrders;
        public RelayCommand<object, object> RemoveSelectedOrders {
            get { return _removeSelectedOrders; }
            set {
                _removeSelectedOrders = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _exportOrder;
        public RelayCommand<object, object> ExportOrder {
            get { return _exportOrder; }
            set {
                _exportOrder = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OrdersViewModel() {
            initializeCommands();
        }
        public OrdersViewModel(bool Initialize) : this() {
            if(Initialize) {
                this.initialize();
            }
        }

        /// <summary>
        /// Initialize the view model.
        /// </summary>
        private void initialize() {
            populateDates();
            populateOrders();
        }

        /// <summary>
        /// Populates the Order date dropdown.
        /// </summary>
        private void populateDates() {
            OrderMonths = new ObservableCollection<DateTime>(DbUtil.GetOrderMonths().OrderBy(o => o).ToList());
            if(OrderMonths.Count() > 0) SelectedMonth = OrderMonths.FirstOrDefault();
        }

        /// <summary>
        /// Populates the order data grid.
        /// </summary>
        private void populateOrders() {
            if(SelectedMonth != null) Orders = new ObservableCollection<OrderDisplayModel>(OrderDisplayModel.GetOrders(SelectedMonth.Value).OrderBy(o => o.Comic.Title).ThenBy(o => o.Customer.FirstName));
        }

        /// <summary>
        /// Initializes the relay commands.
        /// </summary>
        private void initializeCommands() {
            ViewCustomers = new RelayCommand<object>(o => {
                new CustomersView().ShowDialog();
            });
            ViewComics = new RelayCommand<object>(o => {
                new ComicsView().ShowDialog();
            });
            AddOrder = new RelayCommand<object>(o => {
                MessageBox.Show("Hey");
            });
            RemoveSelectedOrders = new RelayCommand<object, object>(o => {
                MessageBox.Show("Hey");
            }, o => {
                return SelectedOrder != null;
            });
            ExportOrder = new RelayCommand<object, object>(o => {
                MessageBox.Show("Hey");
            }, o => {
                return SelectedMonth != null;
            });
        }
    }
}
