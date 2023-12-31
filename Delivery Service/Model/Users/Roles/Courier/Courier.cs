﻿using Delivery_Service.Entities;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Delivery_Service.Model.Users.User.Roles.Courier {
    public class Courier : ICourier {

        public bool HasVehicle { get; set; } = false;

        public List<IOrder>? Orders { get; set; } = new();

        public Guid UserId { get; }


        public Courier(bool HasVehicle, List<IOrder> Orders, Guid UserId) {
            if (UserId == Guid.Empty) throw new ArgumentException("Идентификатор курьера не может быть пустым");
            this.UserId = UserId;
            this.HasVehicle = HasVehicle;
            this.Orders = Orders;
        }

    }
}
