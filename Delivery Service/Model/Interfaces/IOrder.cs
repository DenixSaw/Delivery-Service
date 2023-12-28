using Delivery_Service.Entities;
using Delivery_Service.Model.Users.User.Roles.Courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Delivery_Service.Model.Interfaces {
    //[JsonDerivedType(typeof(Order))]

    public interface IOrder {
        Guid Id { get; }
        DateTime DateOfAdding { get; }
        decimal TotalCost { get; }
        IClient Client { get; }
        string Address { get; set; }
        IList<ICartObject> Products { get; set; }
        Guid Courier { get; set; }
        string Comment { get; set; }
        PaymentMethod PaymentMethod { get; set; }

        OrderStatus OrderStatus { get; set; }

    }
}
