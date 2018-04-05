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
            get { return _orders; }
            set {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private OrderModel _selectedOrder;
        public OrderModel SelectedOrder {
            get { return _selectedOrder; }
            set {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> _orderMonths;
        public ObservableCollection<DateTime> OrderMonths {
            get { return _orderMonths; }
            set {
                _orderMonths = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedMonth;
        public DateTime SelectedMonth {
            get { return _selectedMonth; }
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
            //OrderMonths = new ObservableCollection<OrderMonthDisplayModel>(DbUtil.GetOrderMonths().ToList());
            foreach(var date in DbUtil.GetOrderMonths()) {

            }
        }

        public void PopulateOrders() {
            //Orders = new ObservableCollection<OrderDisplayModel>(OrderDisplayModel.GetOrders(SelectedMonth.OrderMonth).ToList());
        }
    }
}
