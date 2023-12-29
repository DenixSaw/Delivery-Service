using Delivery_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Delivery_Service.Model.Users.User.Roles.Courier;
using Newtonsoft.Json;

namespace Delivery_Service.Data.Repositories {
    public class CourierRepository : IBaseRepository<ICourier> {

        private static readonly string _path = "courier.json";
        private List<ICourier>? _couriers = new();

        public bool Add(ICourier courier) {
            if (courier == null || _couriers?.Find(e => e.UserId == courier.UserId) != null) { return false; }
            _couriers?.Add(courier);
            string json = JsonConvert.SerializeObject(_couriers, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(_path, json);
            return true;
        }

        public bool Delete(ICourier courier) {
            if (_couriers == null) return false;

            if (_couriers.Contains(courier)) {
                _couriers.Remove(courier);
                string json = JsonConvert.SerializeObject(_couriers, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                File.WriteAllText(_path, json);
                return true;
            }
            return false;
        }

        public IList<ICourier>? GetAll() {

            if (File.Exists(_path)) {
                string json = File.ReadAllText(_path);
                List<ICourier>? couriers = JsonConvert.DeserializeObject<List<ICourier>>(json, new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All });
                if (couriers != null) {
                    var result = new List<ICourier>();
                    result.AddRange(couriers);
                    _couriers = result;
                }
            }
            return _couriers;
        }

        public ICourier? GetById(Guid id) {
            return _couriers?.FirstOrDefault(courier => courier.UserId == id);
        }

        public bool Update(ICourier courier) {
            for (int i = 0; i < _couriers?.Count; i++) {
                if (_couriers[i].UserId == courier.UserId) {
                    _couriers[i] = courier;
                    string json = JsonConvert.SerializeObject(_couriers, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                    File.WriteAllText(_path, json);
                    return true;
                }
            }
            return false;
        }

    }
}
