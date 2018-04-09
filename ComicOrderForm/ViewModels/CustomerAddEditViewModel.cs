using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.WPF.ViewModels {
    public class CustomerAddEditViewModel : BaseViewModel {
        #region Properties
        private CustomerModel _customer;
        public CustomerModel Customer {
            get => _customer;
            set {
                _customer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private RelayCommand<object, object> _save;
        public RelayCommand<object, object> Save {
            get => _save;
            set {
                _save = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object> _cancel;
        public RelayCommand<object> Cancel {
            get => _cancel;
            set {
                _cancel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private bool isEdit;

        #region Constructors
        private CustomerAddEditViewModel(bool isEdit) {
            this.isEdit = isEdit;
            initializeCommands();
        }

        public CustomerAddEditViewModel() : this(false) {
            Customer = new CustomerModel();
        }

        public CustomerAddEditViewModel(long CustomerId) : this(true) {
            Customer = DbUtil.GetById<CustomerModel>(CustomerId);
        }
        #endregion

        private void initializeCommands() {
            Save = new RelayCommand<object, object>(o => {
                if(isEdit) DbUtil.Update(Customer);
                else DbUtil.Insert(Customer);
            }, o => {
                return  !String.IsNullOrWhiteSpace(Customer.FirstName) &&
                        !String.IsNullOrWhiteSpace(Customer.LastName) &&
                        !String.IsNullOrWhiteSpace(Customer.Email);
            });
        }
    }
}