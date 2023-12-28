using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users;
using Delivery_Service.Views.Pages.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Delivery_Service.Data.Repositories {
    public class DishRepository : IBaseRepository<IProduct> {
        private static readonly string _path = "dish.json";
        private List<IProduct>? _dishes = new();

        public bool Add(IProduct dish) {
            if (dish == null || _dishes?.Find(d => d.Id == dish.Id) != null) { return false; }
            _dishes?.Add(dish);
            string json = JsonSerializer.Serialize(_dishes, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_path, json);
            return true;
        }

        public bool Delete(IProduct dish) {
            if (_dishes == null || dish == null) return false;

            if (_dishes.Find(d => d.Id == dish.Id) != null) {
                _dishes.Remove(dish);
                string json = JsonSerializer.Serialize(_dishes, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(_path, json);
                return true;
            }
            return false;
        }

        public IList<IProduct>? GetAll() {

            if (File.Exists(_path)) {
                List<Dish>? dishes = JsonSerializer.Deserialize<List<Dish>>(File.ReadAllText(_path));
                if (dishes != null) {
                    var result = new List<IProduct>();
                    result.AddRange(dishes);
                    _dishes = result;
                }
            }
            return _dishes;
        }

        public IProduct? GetById(Guid id) {
            return _dishes?.FirstOrDefault(dish => dish.Id == id);
        }

        public bool Update(IProduct dish) {
            for (int i = 0; i < _dishes?.Count; i++) {
                if (_dishes[i].Id == dish.Id) {
                    _dishes[i] = dish;
                    string json = JsonSerializer.Serialize(_dishes, new JsonSerializerOptions() { WriteIndented = true });
                    File.WriteAllText(_path, json);
                    return true;
                }
            }
            return false;
        }

    }
}

