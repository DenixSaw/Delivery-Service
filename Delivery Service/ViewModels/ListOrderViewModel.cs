using Delivery_Service.Data;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.ViewModels {
    public class ListOrderViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private string _currentUserName = "";
        private string _currentUserRole = "";
        public string CurrentUserRole {
            get { return _currentUserRole; }
            private set {
                _currentUserRole = value;
                OnPropertyChanged(nameof(CurrentUserRole));
            }
        }
        public string CurrentUserName {
            get { return _currentUserName; }
            private set {
                _currentUserName = value;
                OnPropertyChanged(nameof(CurrentUserName));
            }
        }

        public ObservableCollection<IOrder>? Orders { get; set; } = new();
        private IOrder? _selectedOrder;
        public IOrder? SelectedOrder {
            get { return _selectedOrder; }
            set {
                if (value != _selectedOrder) {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }

        public ListOrderViewModel(IDataManager dataManager) {
            _dataManager = dataManager;
            if (_dataManager.CurrentUser != null) {
                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = "(" + _dataManager.CurrentUser.Role + ")";
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }
        }
    }
}
