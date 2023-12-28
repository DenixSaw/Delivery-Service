using Delivery_Service.Data;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users.User.Roles.Courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Services {
    public class OrderCUDInteractor : IBaseCUDInteractor<IOrder> {
        private readonly IDataManager _dataManager;

        public OrderCUDInteractor(IDataManager dataManager) {
            _dataManager = dataManager;
        }

        public IOrder? TryAdd(IOrder newOrder) {
            if (_dataManager.OrderRepository.Add(newOrder)) {
                //Courier courier  = (Courier)_dataManager.CourierRepository.GetById(newOrder.Courier);
                //if (!courier.Orders.Contains(newOrder)) {
                //    courier.Orders.Add(newOrder);
                //}
                //_dataManager.CourierRepository.Update(courier);
                return newOrder; }
            return null;
        }

        public bool TryDelete(IOrder order) {
            if (order != null && _dataManager.OrderRepository.GetById(order.Id) != null) {
                if (_dataManager.OrderRepository.Delete(order)) {
                    return true;
                }
            }
            return false;
        }

        public bool TryUpdate(IOrder order) {
            if (_dataManager.OrderRepository.Update(order)) { return true; }
            return false;
        }
    }
}
