using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using ComicOrders.WPF.Views;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ComicOrders.WPF.ViewModels {
    public class CustomersViewModel : BaseViewModel {
        #region Properties
        public override string Title => "Customers";

        private ObservableCollection<CustomerModel> _customers;
        public ObservableCollection<CustomerModel> Customers {
            get => _customers;
            set {
                _customers = value;
                OnPropertyChanged();
            }
        }

       private IList _selectedCustomers = new ArrayList();
        public IList SelectedCustomers {
            get => _selectedCustomers;
            set {
                _selectedCustomers = value;
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
                new CustomerAddEditView(((CustomerModel)SelectedCustomers[0]).Id).ShowDialog();
                populateCustomers();
            }, o => SelectedCustomers.Count == 1);

            RemoveSelectedCustomers = new RelayCommand<object, object>(o => {
                //Confirm
                var confirmBox = MessageBox.Show("Are you sure you want to remove these customers? This will remove any orders associated with that customer as well.", "Delete Customers", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                //Delete
                if(confirmBox == MessageBoxResult.OK)
                    DbUtil.Delete(SelectedCustomers.Cast<CustomerModel>().ToList());
                
                populateCustomers();
            }, o => SelectedCustomers.Count > 0);
        }
    }
}