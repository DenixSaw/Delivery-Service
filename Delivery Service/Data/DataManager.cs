using Delivery_Service.Data.Repositories;
using Delivery_Service.Entities;
using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Model.Users;
using Delivery_Service.Model.Users.Roles.Admin;
using Delivery_Service.Model.Users.User.Roles.Courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Data {
    public class DataManager : IDataManager {

        private IUser? _currentUser = null;
        public IUser? CurrentUser {
            get => _currentUser;
            set {
                if (value != null && _userRepository.GetById(value.Id) != null) {
                    _currentUser = value;
                }
            }

        }

        private readonly IBaseRepository<IUser> _userRepository;
        public IBaseRepository<IUser> UserRepository => _userRepository;

        private readonly IBaseRepository<IAdmin> _adminRepository;

        private readonly IBaseRepository<IProduct> _dishRepository;
        public IBaseRepository<IProduct> DishRepository => _dishRepository;
        public IBaseRepository<IAdmin> AdminRepository => _adminRepository;

        private readonly IBaseRepository<ICourier> _courierRepository;
        public IBaseRepository<ICourier> CourierRepository => _courierRepository;

        private readonly IBaseRepository<IOrder> _orderRepository;
        public IBaseRepository<IOrder> OrderRepository => _orderRepository;

        public DataManager(IBaseRepository<IAdmin> adminRepository, IBaseRepository<IUser> userRepository, IBaseRepository<ICourier> courierRepository, IBaseRepository<IProduct> dichRepository, IBaseRepository<IOrder> orderRepository) {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _courierRepository = courierRepository;
            _dishRepository = dichRepository;
            _orderRepository = orderRepository;
        }
    }
}


