using Delivery_Service.Model;
using Delivery_Service.Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Delivery_Service.Data.Repositories {
    public class UserRepository : IBaseRepository<IUser> {
        private static readonly string _path = "user.json";
        private List<IUser>? _users = new();

        public bool Add(IUser user) {
            if (user == null || _users?.Find(e => e.Id == user.Id) != null) { return false; }
            _users?.Add(user);
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(_path, json);
            return true;
        }

        public bool Delete(IUser user) {
            if (_users == null) return false;

            if (_users.Contains(user)) {
                _users.Remove(user);
                string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(_path, json);
                return true;
            }
            return false;
        }

        public IList<IUser>? GetAll() {

            if (File.Exists(_path)) {
                List<User>? users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_path));
                if (users != null) {
                    var result = new List<IUser>();
                    result.AddRange(users);
                    _users = result;
                }
            }
            return _users;
        }

        public IUser? GetById(Guid id) {
            return _users?.FirstOrDefault(user => user.Id == id);
        }

        public bool Update(IUser user) {
            for (int i = 0; i < _users?.Count; i++) {
                if (_users[i].Id == user.Id) {
                    _users[i] = user;
                    string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions() { WriteIndented = true });
                    File.WriteAllText(_path, json);
                    return true;
                }
            }
            return false;
        }

    }
}
