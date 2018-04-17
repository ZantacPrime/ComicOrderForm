using ComicOrders.DB;
using ComicOrders.DB.Models;
using ComicOrders.Lib;
using ComicOrders.WPF.Views;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Reflection;

namespace ComicOrders.WPF.ViewModels {
    public class ComicsViewModel : BaseViewModel {
        #region Properties
        public override string Title => "Comics";

        private ObservableCollection<ComicModel> _comics;
        public ObservableCollection<ComicModel> Comics {
            get => _comics;
            set {
                _comics = value;
                OnPropertyChanged();
            }
        }

        private IList _selectedComics = new ArrayList();
        public IList SelectedComics {
            get => _selectedComics;
            set {
                _selectedComics = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private RelayCommand<object> _addComic;
        public RelayCommand<object> AddComic {
            get => _addComic;
            set {
                _addComic = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _editSelectedComic;
        public RelayCommand<object, object> EditSelectedComic {
            get => _editSelectedComic;
            set {
                _editSelectedComic = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand<object, object> _removeSelectedComics;
        public RelayCommand<object, object> RemoveSelectedComics {
            get => _removeSelectedComics;
            set {
                _removeSelectedComics = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ComicsViewModel() {
            initializeCommands();
            populateComics();
        }

        private void initializeCommands() {
            AddComic = new RelayCommand<object>(o => {
                new ComicsAddEditView().ShowDialog();
                populateComics();
            });

            EditSelectedComic = new RelayCommand<object, object>(o => {
                new ComicsAddEditView(((ComicModel)SelectedComics[0]).Id).ShowDialog();
                populateComics();
            }, o => {
                return SelectedComics.Count == 1;
            });
        

            RemoveSelectedComics = new RelayCommand<object, object>(o => {
                var confirmBox = MessageBox.Show("Are you sure you want to remove these comics? This will remove any orders associated with that comic as well.", "Delete Comics", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(confirmBox == MessageBoxResult.OK)
                    DbUtil.Delete(SelectedComics.Cast<ComicModel>().ToList());

                populateComics();
            }, o => SelectedComics.Count > 0);
        }

        private void populateComics(PropertyInfo Property = null) {
            if(Property is null)
                Property = typeof(ComicModel).GetProperty(nameof(ComicModel.Title));

            Comics = new ObservableCollection<ComicModel>(DbUtil.GetAll<ComicModel>().OrderBy(o => Property.GetValue(o)).Take(1000).ToList());
        }
    }
}
