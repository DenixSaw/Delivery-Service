using Delivery_Service.Commands;
using Delivery_Service.Data;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users;
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
    internal class ListDishViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private IBaseCUDInteractor<IProduct> _dishCUDInteractor;
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

        public ICommand DeleteDishCommand { get; }
        public ICommand TryAddNewDishCommand { get; }

        public event Action? TryAddNewDish;

        public ListDishViewModel(IDataManager dataManager) {
            DeleteDishCommand = new RelayCommand(DeleteSelectedDish);
            TryAddNewDishCommand = new RelayCommand(TryAddDish);

            if (dataManager != null && dataManager.CurrentUser != null) {
                _dataManager = dataManager;
                _dishCUDInteractor = new DishCUDInteractor(dataManager);

                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = "(" + _dataManager.CurrentUser.Role + ")";

                ObservableCollection<IProduct> dishes = new(_dataManager.DishRepository.GetAll());
                Dishes = dishes;
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }
        }

        private void DeleteSelectedDish() {
            if (SelectedDish != null && _dishCUDInteractor.TryDelete(SelectedDish)) {
                Dishes?.Remove(SelectedDish);
            } else { MessageBox.Show("Ошибка при удалении блюда из списка"); }
        }

        private void TryAddDish() {
            TryAddNewDish?.Invoke();
        }

    }
}
