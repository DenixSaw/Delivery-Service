using Delivery_Service.Data;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users;
using Delivery_Service.Services;
using Delivery_Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.ViewModels {
    public class AddOrderViewModel : BaseViewModel {
        private IDataManager _dataManager;

        private string _currentUserName;
        private string _currentUserRole;
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

        public ObservableCollection<IProduct>? Dishes { get; set; } = new();
        private IProduct? _selectedDish;
        public IProduct? SelectedDish {
            get { return _selectedDish; }
            set {
                if (value != _selectedDish) {
                    _selectedDish = value;
                    OnPropertyChanged(nameof(SelectedDish));
                }
            }
        }

        public ObservableCollection<IUser>? Couriers { get; set; } = new();
        private IUser? _selectedCourier;
        public IUser? SelectedCourier {
            get { return _selectedCourier; }
            set {
                if (value != _selectedCourier) {
                    _selectedCourier = value;
                    OnPropertyChanged(nameof(SelectedCourier));
                }
            }
        }

        public AddOrderViewModel(IDataManager dataManager) {
            if (dataManager != null && dataManager.CurrentUser != null) {
                _dataManager = dataManager;

                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = "(" + _dataManager.CurrentUser.Role + ")";

                ObservableCollection<IProduct> dishes = new(_dataManager.DishRepository.GetAll());
                Dishes = dishes;

                ObservableCollection<IUser> couriers = new();
                _dataManager.CourierRepository.GetAll();
                foreach (var user in _dataManager.UserRepository.GetAll()) {
                    if (_dataManager.CourierRepository.GetById(user.Id) != null) {
                        couriers.Add(user);
                    }
                }
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }
        }
    }
}
