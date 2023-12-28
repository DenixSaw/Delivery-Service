using Delivery_Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery_Service.Model;
using System.Text.Json.Serialization;

namespace Delivery_Service.Model.Interfaces
{
    //[JsonDerivedType(typeof(Client))]
    public interface IClient
    {
        string Name { get; set; }
        string Phone { get; set; }
    }
}
