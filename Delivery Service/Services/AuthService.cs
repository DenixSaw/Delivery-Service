using Delivery_Service.Data;
using Delivery_Service.Data.Repositories;
using Delivery_Service.Model.Users.Roles.Admin;
using Delivery_Service.Model.Users.User.Roles.Courier;
using Delivery_Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Delivery_Service.Services {
    public class AuthService : IAuthService {
        private IDataManager _dataManager;

        public AuthService(IDataManager dataManager) {
        _dataManager = dataManager;
        }

        public bool TrySignIn(string phone, string password, string role) {
            var users = _dataManager.UserRepository.GetAll();
            if (users == null) { return false; }
            foreach (var user in users) {
                if (user.Phone == phone && user.Password == password && user.Role == role) {
                    _dataManager.CurrentUser = user;
                    return true;
                }
            }
            return false;
        }

        public bool TrySignUp(string name, string phone, string password, string repeatedPassword, string role) {
            var users = _dataManager.UserRepository.GetAll();

            if (users != null) {
                foreach (var user in users) {
                    if (user.Phone.Contains(phone) && user.Role == role) { return false; }
                }
            }

            if (role == "Admin") {
                Guid userId = Guid.NewGuid();
                User newUser = new(userId, phone, name, password, role);
                Admin admin = new(userId);
                if (newUser != null) { return (_dataManager.UserRepository.Add(newUser) && _dataManager.AdminRepository.Add(admin)); }
            }

            if (role == "Courier") {
                Guid userId = Guid.NewGuid();
                User newUser = new(userId, phone, name, password, role);
                Courier courier = new(false, null, userId);
                if (newUser != null) { return (_dataManager.UserRepository.Add(newUser) && _dataManager.CourierRepository.Add(courier)); }

            }
            return false;
        }
    }
}
