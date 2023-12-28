using Delivery_Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery_Service.Model;

namespace Delivery_Service.Model.Interfaces
{
    public interface IClient
    {
        string Name { get; set; }
        string Phone { get; set; }
    }
}
