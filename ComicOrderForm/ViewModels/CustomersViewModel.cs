using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using ComicOrders.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.WPF.ViewModels {
    public class CustomersViewModel : BaseViewModel {
        #region Properties
        private ObservableCollection<CustomerModel> _customers;
        public ObservableCollection<CustomerModel> Customers {
            get => _customers;
            set {
                _customers = value;
                OnPropertyChanged();
            }
        }

        private CustomerModel _selectedCustomer;
        private CustomerModel SelectedCustomer {
            get => _selectedCustomer;
            set {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private RelayCommand<object> _addCustomer;
        public RelayCommand<object> AddCustomer {
            get => _addCustomer;
            set {
                _addCustomer = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _editSelectedCustomer;
        public RelayCommand<object, object> EditSelectedCustomer {
            get => _editSelectedCustomer;
            set {
                _editSelectedCustomer = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _removeSelectedCustomers;
        public RelayCommand<object, object> RemoveSelectedCustomers {
            get => _removeSelectedCustomers;
            set {
                _removeSelectedCustomers = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public CustomersViewModel() {
            initializeCommands();
            populateCustomers();
        }

        public void populateCustomers() {
            Customers = new ObservableCollection<CustomerModel>(DbUtil.GetAll<CustomerModel>());
        }

        public void initializeCommands() {
            AddCustomer = new RelayCommand<object>(o => {
                new CustomerAddEditView().ShowDialog();
                populateCustomers();
            });

            EditSelectedCustomer = new RelayCommand<object, object>(o => {
                new CustomerAddEditView(SelectedCustomer.Id).ShowDialog();
                populateCustomers();
            }, o => SelectedCustomer != null);

            RemoveSelectedCustomers = new RelayCommand<object, object>(o => {
                //Confirm
                //Delete
                populateCustomers();
            }, o => SelectedCustomer != null);
        }
    }
}
