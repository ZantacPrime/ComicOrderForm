﻿using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComicOrders.WPF.ViewModels {
    public class CustomerAddEditViewModel : BaseViewModel,IAddEditViewModel {
        #region Properties
        private CustomerModel _customer;
        public CustomerModel Customer {
            get => _customer;
            set {
                _customer = value;
                OnPropertyChanged();
            }
        }

        public override string Title { get {
                if(isEdit) return "Edit Customer";
                return "Add Customer";
            } }
        #endregion

        #region Commands
        private RelayCommand<Window, object> _save;
        public RelayCommand<Window, object> Save {
            get => _save;
            set {
                _save = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<Window> _cancel;
        public RelayCommand<Window> Cancel {
            get => _cancel;
            set {
                _cancel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool isEdit { get; }

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
            Save = new RelayCommand<Window, object>(o => {
                if(isEdit) DbUtil.Update(Customer);
                else DbUtil.Insert(Customer);
                o.Close();
            }, o => !String.IsNullOrWhiteSpace(Customer.FirstName) &&
                    !String.IsNullOrWhiteSpace(Customer.LastName) &&
                    !String.IsNullOrWhiteSpace(Customer.Email));

            Cancel = new RelayCommand<Window>(w => w.Close());
        }


    }
}