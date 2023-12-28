using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Delivery_Service.Model.Interfaces;

namespace Delivery_Service.Model.Users.User.Roles.Courier {
    //[JsonDerivedType(typeof(Courier))]
    public interface ICourier {
        bool HasVehicle { get; set; }
        List<IOrder>? Orders { get; set; }
        public Guid UserId { get; }
    }

}

