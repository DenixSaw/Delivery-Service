using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Delivery_Service.Model.Interfaces {
    //[JsonDerivedType (typeof(CartObject))]
    public interface ICartObject {
        IProduct Product { get; set; }
        int Quantity { get; set; }
    }
}
