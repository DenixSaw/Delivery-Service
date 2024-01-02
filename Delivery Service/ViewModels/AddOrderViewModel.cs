using Delivery_Service.Commands;
using Delivery_Service.Data;
using Delivery_Service.Entities;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users;
using Delivery_Service.Services;
using Delivery_Service.Utils;
using Delivery_Service.ViewModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Delivery_Service.ViewModels {
    public class AddOrderViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private IBaseCUDInteractor<IOrder> _orderCUDInteractor;

        private string _currentUserName = "Error";
        private string _currentUserRole = "Error";
        public string CurrentUserRole {
            get { return _currentUserRole; }
            private set {
                _currentUserRole = RoleConverter.ConvertRole(value);
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

        private string _paymentMethod = "CashPayment";

        public string PaymentMethod {
            get { return _paymentMethod; }
            private set {
                _paymentMethod = value;
                OnPropertyChanged(nameof(PaymentMethod));
            }
        }
        private PaymentMethod _modelPaymentMethod = Entities.PaymentMethod.CashPayment;

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

        private string _address = "";
        public string Address {
            get { return _address; }
            set {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _clientName = "";

        public string ClientName {
            get { return _clientName; }
            set {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }

        private string _clientPhone = "";

        public string ClientPhone {
            get { return _clientPhone; }
            set {
                _clientPhone = value;
                OnPropertyChanged(nameof(ClientPhone));
            }
        }

        private string _comment = "";
        public string Comment {
            get { return _comment; }
            set {
                _comment = value;
                OnPropertyChanged(nameof(Comment));

            }
        }

        private decimal _cost = 0;
        public decimal Cost {
            get { return _cost; }
            set {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private ObservableCollection<ICartObject> _quantityDishMap = new();

        public ObservableCollection<ICartObject> QuantityDishMap {
            get { return _quantityDishMap; }
            set {
                _quantityDishMap = value;
                OnPropertyChanged(nameof(QuantityDishMap));
            }
        }

        private ICalculator _calculator;
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand SetPaymentMethodCommand { get; }
        public ICommand TryAddOrderCommand { get; }

        public AddOrderViewModel(IDataManager dataManager) {
            if (dataManager != null && dataManager.CurrentUser != null) {
                _dataManager = dataManager;
                _orderCUDInteractor = new OrderCUDInteractor(_dataManager);
                _calculator = new CostOrderCalculator();


                //Установка текущего пользователя в углу окна
                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = RoleConverter.ConvertRole(_dataManager.CurrentUser.Role);

                //Заполнение мапы для хранения блюд
                ObservableCollection<IProduct>? dishes = new(_dataManager?.DishRepository.GetAll());
                ObservableCollection<ICartObject> quantityMap = new();

                if (dishes != null) {
                    foreach (var dish in dishes) {
                        quantityMap.Add(new CartObject(dish, 0));
                    }
                    QuantityDishMap = quantityMap;
                }

                //Команды на "+/-"
                IncreaseQuantityCommand = new Command(IncreaseQuantity, canexecutemethod => true);
                DecreaseQuantityCommand = new Command(DecreaseQuantity, canexecutemethod => true);

                //Другие команды
                SetPaymentMethodCommand = new Command(SetPaymentMethod, canexecutemethod => PaymentMethod != null);
                TryAddOrderCommand = new RelayCommand(TryAddOrder);

                //Установка списка курьеров на форме
                ObservableCollection<IUser> couriers = new();
                if (_dataManager.CourierRepository.GetAll() != null) {

                    foreach (var user in _dataManager.UserRepository.GetAll()) {
                        if (_dataManager.CourierRepository.GetById(user.Id) != null) {
                            couriers.Add(user);
                        }
                    }
                    Couriers = couriers;
                }
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }
        }

        private void IncreaseQuantity(object item) {
            ObservableCollection<ICartObject> updatedMap = _quantityDishMap;

            if (item is CartObject pair) {
                for (int i = 0; i < updatedMap.Count; i++) {
                    if (updatedMap[i].Product == pair.Product) {
                        int newQuantity = updatedMap[i].Quantity + 1;
                        updatedMap[i] = new CartObject(pair.Product, newQuantity);
                        QuantityDishMap = updatedMap;
                        break;
                    }
                }
                Cost = _calculator.Calculate(QuantityDishMap);
            }

        }

        private void DecreaseQuantity(object item) {
            ObservableCollection<ICartObject> updatedMap = _quantityDishMap;

            if (item is CartObject pair && pair.Quantity >= 0) {
                for (int i = 0; i < updatedMap.Count; i++) {
                    if (updatedMap[i].Product == pair.Product) {
                        int newQuantity = updatedMap[i].Quantity - 1;
                        if (newQuantity < 0) { newQuantity = 0; }
                        updatedMap[i] = new CartObject(pair.Product, newQuantity);
                        QuantityDishMap = updatedMap;
                        break;
                    }
                }
                Cost = _calculator.Calculate(QuantityDishMap);
            }

        }

        private void SetPaymentMethod(object item) {
            string paymentMethod = (string)item;
            PaymentMethod = paymentMethod;
            if (paymentMethod == "CashPayment") {
                _modelPaymentMethod = Entities.PaymentMethod.CashPayment;
            } else if (paymentMethod == "PaymentByCard") {
                _modelPaymentMethod = Entities.PaymentMethod.PaymentByCard;
            } else if (paymentMethod == "PaymentByCardToCourier") {
                _modelPaymentMethod = Entities.PaymentMethod.PaymentByCardToCourier;
            }
        }

        private void TryAddOrder() {
            List<ICartObject> cart = new();

            foreach (CartObject item in _quantityDishMap.Cast<CartObject>()) {
                if (item != null && item.Quantity > 0) {
                    cart.Add(item);
                }
            }
            Cost = _calculator.Calculate(cart);


            string errorMessage = "";
            Regex phoneRegex = new(@"^\+?[0-9]{11}$");
            if (_address == "") errorMessage += "Не заполнен адрес\n";
            if (_clientName == "") errorMessage += "Не заполнено имя клиента\n";
            if (!phoneRegex.IsMatch(_clientPhone)) errorMessage += "Некорректный телефон клиента\n";
            if (_selectedCourier == null) errorMessage += "Курьер не назначен\n";
            if (cart.Count == 0) errorMessage += "Ни одно блюдо не добавлено в заказ\n";
            if (errorMessage == "") {
                Order order = new(
                    Guid.NewGuid(),
                    DateTime.Now,
                    cart,
                    _address,
                    new Client(_clientName, _clientPhone),
                    _comment,
                    _modelPaymentMethod,
                    _selectedCourier.Id,
                    OrderStatus.New,
                    Cost);
                if (_orderCUDInteractor.TryAdd(order) != null) { MessageBox.Show("Заказ добавлен"); } else MessageBox.Show("Ошибка при добавлении заказа");
            } else MessageBox.Show(errorMessage); 
        }

    }
}

