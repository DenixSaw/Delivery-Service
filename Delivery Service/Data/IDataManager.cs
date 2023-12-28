using Delivery_Service.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery_Service.Model.Users;
using Delivery_Service.Model;
using Delivery_Service.Model.Users.User.Roles.Courier;
using Delivery_Service.Model.Users.Roles.Admin;
using Delivery_Service.Model.Interfaces;

namespace Delivery_Service.Data {
    public interface IDataManager {
        public IUser? CurrentUser { get; set; }
        public IBaseRepository<IUser> UserRepository { get; }
        public IBaseRepository<ICourier> CourierRepository { get; }
        public IBaseRepository<IAdmin> AdminRepository { get; }

        public IBaseRepository<IProduct> DishRepository { get; }

    }
}
