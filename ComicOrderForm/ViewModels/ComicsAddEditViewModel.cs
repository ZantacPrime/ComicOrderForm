using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using System;
using System.Windows;

namespace ComicOrders.WPF.ViewModels {
    public class ComicsAddEditViewModel : BaseViewModel, IAddEditViewModel {
        #region Properties
        public bool isEdit { get; }

        public override string Title {
            get {
                if(isEdit) return "Edit Comic";
                return "Add Comic";
            }
        }

        private ComicModel _comic;
        public ComicModel Comic {
            get => _comic;
            set {
                _comic = value;
                OnPropertyChanged();
            }
        }
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

        #region Constructors
        private ComicsAddEditViewModel(bool isEdit) {
            this.isEdit = isEdit;
            initializeCommands();
        }

        public ComicsAddEditViewModel():this(false) {
            Comic = new ComicModel();
            Comic.ReleaseDate = DateTime.Now.Date;
        }

        public ComicsAddEditViewModel(long ComicId):this(true) {
            Comic = DbUtil.GetById<ComicModel>(ComicId);
        }
        #endregion

        private void initializeCommands() {
            Save = new RelayCommand<Window, object>(w => {
                if(isEdit) DbUtil.Update(Comic);
                else DbUtil.Insert(Comic);
                w.Close();
            }, o => Comic.HasRequiredFields());

            Cancel = new RelayCommand<Window>(w => {
                w.Close();
            });
        }
    }
}
