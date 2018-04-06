using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.WPF.ViewModels {
    public class OrdersViewModel : BaseViewModel {

        #region Properties
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

        private DateTime _selectedMonth;
        public DateTime SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OrdersViewModel() {  }
        public OrdersViewModel(bool Initialize) {
            if(Initialize) {
                this.Initialize();
            }
        }

        public void Initialize() {
            PopulateDates();

            PopulateOrders();
        }

        public void PopulateDates() {
            OrderMonths = new ObservableCollection<DateTime>(DbUtil.GetOrderMonths().OrderBy(o => o).ToList());
            SelectedMonth = OrderMonths.FirstOrDefault();
        }

        public void PopulateOrders() {
            Orders = new ObservableCollection<OrderDisplayModel>(OrderDisplayModel.GetOrders(SelectedMonth).OrderBy(o => o.Comic.Title).ThenBy(o => o.Customer.FirstName));
        }
    }
}
