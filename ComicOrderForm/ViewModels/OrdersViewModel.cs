using ComicOrderForm.Helpers;
using ComicOrderForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrderForm.ViewModels {
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

        private ObservableCollection<OrderMonthDisplayModel> _orderMonths;
        public ObservableCollection<OrderMonthDisplayModel> OrderMonths {
            get { return _orderMonths; }
            set {
                _orderMonths = value;
                OnPropertyChanged();
            }
        }

        private OrderMonthDisplayModel _selectedMonth;
        public OrderMonthDisplayModel SelectedMonth {
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
            OrderMonths = new ObservableCollection<OrderMonthDisplayModel>(DbUtil.GetOrderMonths().ToList());
            foreach(var date in DbUtil.GetOrderMonths()) {

            }
        }

        public void PopulateOrders() {
            Orders = new ObservableCollection<OrderDisplayModel>(OrderDisplayModel.GetOrders(SelectedMonth.OrderMonth).ToList());
        }
    }
}
