using Delivery_Service.Commands;
using Delivery_Service.Data;
using Delivery_Service.Entities;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Services;
using Delivery_Service.Utils;
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
    public class ListOrderViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private IBaseCUDInteractor<IOrder> _orderCUDInteractor;
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


        private string _courier = "";
        public string Courier {
            get { return _courier; }
            set {
                _courier = value;
                OnPropertyChanged(nameof(Courier));
            }
        }

        private string _paymentMethod = "";
        public string PaymentMethod {
            get { return _paymentMethod; }
            private set {
                _paymentMethod = value;
                OnPropertyChanged(nameof(PaymentMethod));
            }
        }

        //Статус, который присваивается выбранному заказу при нажатии на кнопку "Установить статус"
        private string _newOrderStatus = "New";
        public string NewOrderStatus {
            get { return _newOrderStatus; }
            private set {
                _newOrderStatus = value;
                OnPropertyChanged(nameof(NewOrderStatus));
                if (SelectedOrder != null) SelectedOrder.OrderStatus = ConvertOrderStatusToEnum(_newOrderStatus);

            }
        }

        private string _orderStatus = "";
        public string OrderStatus {
            get { return _orderStatus; }
            private set {
                _orderStatus = value;
                OnPropertyChanged(nameof(OrderStatus));
            }
        }
        public ObservableCollection<IOrder>? Orders { get; set; } = new();
        private IOrder? _selectedOrder;
        public IOrder? SelectedOrder {
            get { return _selectedOrder; }
            set {
                if (value != _selectedOrder) {
                    _selectedOrder = value;
                    Courier = _dataManager.UserRepository.GetById(_selectedOrder.Courier).Name;
                    PaymentMethod = ConvertPaymentMethod(_selectedOrder.PaymentMethod);
                    OrderStatus = ConvertOrderStatus(_selectedOrder.OrderStatus);
                    OnPropertyChanged(nameof(SelectedOrder));

                }
            }
        }

        public ICommand DeleteOrderCommand { get; }

        //Команда устанавливает новое значение статуса из радио кнопки, которое хранится в виде строки в NewOrderStatus
        public ICommand SetNewOrderStatusCommand { get; }
        //Команда, срабатывающая при нажатии на кнопку "Установить статус"
        public ICommand SetActualOrderStatusCommand { get; }
        public ListOrderViewModel(IDataManager dataManager) {
            _dataManager = dataManager;

            if (_dataManager != null && _dataManager.CurrentUser != null) {
                _orderCUDInteractor = new OrderCUDInteractor(_dataManager);

                DeleteOrderCommand = new RelayCommand(DeleteOrder);
                SetNewOrderStatusCommand = new Command(SetNewOrderStatus, canexecutemethod => NewOrderStatus != null);
                SetActualOrderStatusCommand = new RelayCommand(SetActualOrderStatus);

                CurrentUserName = _dataManager.CurrentUser.Name;
                CurrentUserRole = RoleConverter.ConvertRole(_dataManager.CurrentUser.Role);

                _dataManager.CourierRepository.GetAll();
                _dataManager.UserRepository.GetAll();

                ObservableCollection<IOrder> orders = new(_dataManager.OrderRepository.GetAll());
                Orders = orders;
            } else { throw new ArgumentException("Ошибка при установлении текущего пользователя"); }
        }

        private static string ConvertPaymentMethod(PaymentMethod paymentMethod) {
            if (paymentMethod == Entities.PaymentMethod.PaymentByCard) return "Картой";
            if (paymentMethod == Entities.PaymentMethod.CashPayment) return "Наличные";
            if (paymentMethod == Entities.PaymentMethod.PaymentByCardToCourier) return "Картой курьеру";
            return "Error";
        }

        private static string ConvertOrderStatus(OrderStatus orderStatus) {
            if (orderStatus == Entities.OrderStatus.New) return "Новый";
            if (orderStatus == Entities.OrderStatus.Delivered) return "Доставлен";
            return "Error";
        }

        private static OrderStatus ConvertOrderStatusToEnum(string orderStatus) {
            if (orderStatus == "New") return Entities.OrderStatus.New;
            if (orderStatus == "Delivered") return Entities.OrderStatus.Delivered;
            return Entities.OrderStatus.New;
        }

        private void DeleteOrder() {
            if (SelectedOrder != null && _orderCUDInteractor.TryDelete(SelectedOrder)) {
                Orders?.Remove(SelectedOrder);
            } else { MessageBox.Show("Ошибка при удалении заказа"); }
        }

        private void SetNewOrderStatus(object item) {
            string newStatus = (string)item;
            NewOrderStatus = newStatus;

        }

        private void SetActualOrderStatus() {
            if (SelectedOrder != null && OrderStatus != NewOrderStatus && NewOrderStatus != "New") {
                Order? updatedOrder = SelectedOrder as Order;
                updatedOrder.OrderStatus = ConvertOrderStatusToEnum(NewOrderStatus);
                if (_orderCUDInteractor != null && _orderCUDInteractor.TryUpdate(updatedOrder)) {
                    SelectedOrder.OrderStatus = ConvertOrderStatusToEnum(NewOrderStatus);
                    OrderStatus = ConvertOrderStatus(SelectedOrder.OrderStatus);
                }

            }
        }
    }
}
