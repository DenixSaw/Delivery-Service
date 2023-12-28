using Delivery_Service.Entities;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Delivery_Service.Data.Repositories {
    public class OrderRepository: IBaseRepository<IOrder> {
        private static readonly string _path = "order.json";
        private List<IOrder>? _orders = new();

        public bool Add(IOrder order) {
            if (order == null || _orders?.Find(d => d.Id == order.Id) != null) { return false; }
            _orders?.Add(order);
            string json = JsonSerializer.Serialize(_orders, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_path, json);
            return true;
        }

        public bool Delete(IOrder order) {
            if (_orders == null || order == null) return false;

            if (_orders.Find(d => d.Id == order.Id) != null) {
                _orders.Remove(order);
                string json = JsonSerializer.Serialize(_orders, new JsonSerializerOptions() { WriteIndented = true });
                MessageBox.Show(json);
                File.WriteAllText(_path, json);
                return true;
            }
            return false;
        }

        public IList<IOrder>? GetAll() {

            if (File.Exists(_path)) {
                List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(File.ReadAllText(_path));
                if (orders != null) {
                    var result = new List<IOrder>();
                    result.AddRange(orders);
                    _orders = result;
                }
            }
            return _orders;
        }

        public IOrder? GetById(Guid id) {
            return _orders?.FirstOrDefault(order => order.Id == id);
        }

        public bool Update(IOrder order) {
            for (int i = 0; i < _orders?.Count; i++) {
                if (_orders[i].Id == order.Id) {
                    _orders[i] = order;
                    string json = JsonSerializer.Serialize(_orders, new JsonSerializerOptions() { WriteIndented = true });
                    File.WriteAllText(_path, json);
                    return true;
                }
            }
            return false;
        }
    }
}
