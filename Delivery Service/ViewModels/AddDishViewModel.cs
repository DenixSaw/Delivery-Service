using Delivery_Service.Data;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Services;
using Delivery_Service.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Delivery_Service.ViewModels {
    public class AddDishViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private IBaseCUDInteractor<IProduct> _dishCUDInteractor;
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

        private ObservableCollection<IProduct>? _dishes = new();
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

        private string _dishName = "";
        public string DishName {
            get { return _dishName; }
            set {
                _dishName = value;
                OnPropertyChanged(nameof(DishName));
            }
        }

        private string _dishTitle = "";
        public string DishTitle {
            get { return _dishTitle; }
            set {
                _dishTitle = value;
                OnPropertyChanged(nameof(DishTitle));
            }
        }

        private int _dishWeight = 0;
        public int DishWeight {
            get { return _dishWeight; }
            set {
                _dishWeight = value;
                OnPropertyChanged(nameof(DishWeight));
            }
        }

        private decimal _dishPrice = 0;
        public decimal DishPrice {
            get { return _dishPrice; }
            set {
                _dishPrice = value;
                OnPropertyChanged(nameof(DishPrice));
            }
        }

        public ICommand TryAddNewDishCommand { get; }

        public AddDishViewModel(IDataManager dataManager) {
            TryAddNewDishCommand = new RelayCommand(TryAddNewDish);
            if ( dataManager != null && dataManager.CurrentUser != null) {
                _dataManager = dataManager;
                _dishCUDInteractor = new DishCUDInteractor(dataManager);

                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = "(" + _dataManager.CurrentUser.Role + ")";
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }

            ObservableCollection<IProduct> dishes = new(_dataManager.DishRepository.GetAll());
            Dishes = dishes;
        }

        private void TryAddNewDish() {
            string errorMessage = "";
            if (_dishName.Length <= 0) {
                errorMessage += "Название блюда не может быть пустым\n";
            }
            if (_dishWeight < 1) {
                errorMessage += "Вес блюда не может быть меньше 1 грамма\n";
            }
            if (_dishPrice <= 0) {
                errorMessage += "Цена блюда не может быть отрицательной или равной нулю\n";
            }
            if (errorMessage.Length != 0) {
                MessageBox.Show(errorMessage);
            } else {
                Dish newDish = new(Guid.NewGuid(), _dishName, _dishTitle, _dishWeight, _dishPrice);
                if (_dishCUDInteractor.TryAdd(newDish) != null) {
                    Dishes?.Add(newDish);
                }
            }
        }

    }
}
