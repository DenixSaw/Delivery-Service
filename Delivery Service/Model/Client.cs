using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Model
{
    public class Client : IClient
    {

        public string Name { get; set; }
        public string Phone { get; set; }

        public Client(string Name, string Phone)
        {
            this.Phone = Phone;
            this.Name = Name;
        }

    }
}
