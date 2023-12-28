using System;
using System.Collections.Generic;
using System.Linq;
using Delivery_Service.Model.Users;
using Delivery_Service.Model;
using System.Text.Json;
using Delivery_Service.Model.Users.Roles.Admin;
using System.IO;

namespace Delivery_Service.Data.Repositories {
    public class AdminRepository : IBaseRepository<IAdmin> {
        private static readonly string _path = "admin.json";
        private List<IAdmin>? _admins = new();

        public bool Add(IAdmin admin) {
            if (admin == null || _admins?.Find(e => e.UserId == admin.UserId) != null) { return false; }
            _admins?.Add(admin);
            string json = JsonSerializer.Serialize(_admins, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_path, json);
            return true;
        }

        public bool Delete(IAdmin admin) {
            if (_admins == null) return false;

            if (_admins.Contains(admin)) {
                _admins.Remove(admin);
                string json = JsonSerializer.Serialize(_admins, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(_path, json);
                return true;
            }
            return false;
        }

        public IList<IAdmin>? GetAll() {

            if (File.Exists(_path)) {
                List<Admin>? admins = JsonSerializer.Deserialize<List<Admin>>(File.ReadAllText(_path));
                if (admins != null) {
                    var result = new List<IAdmin>();
                    result.AddRange(admins);
                    _admins = result;
                }
            }
            return _admins;
        }

        public IAdmin? GetById(Guid id) {
            return _admins?.FirstOrDefault(admin => admin.UserId == id);
        }

        public bool Update(IAdmin admin) {
            for (int i = 0; i < _admins?.Count; i++) {
                if (_admins[i].UserId == admin.UserId) {
                    _admins[i] = admin;
                    string json = JsonSerializer.Serialize(_admins, new JsonSerializerOptions() { WriteIndented = true });
                    File.WriteAllText(_path, json);
                    return true;
                }
            }
            return false;
        }

    }
}
