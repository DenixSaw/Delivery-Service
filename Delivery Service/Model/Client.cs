using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delivery_Service.Model
{
    public class Client : IClient
    {
        private static Regex phoneRegex = new(@"^\+?[0-9]{11}$");
        public string Name { get; set; }
        public string Phone { get; set; }

        public Client(string Name, string Phone)
        {
            if (Name == null || Name.Length == 0) throw new ArgumentException("Имя клиента не может быть пустым");
            if (!phoneRegex.IsMatch(Phone)) throw new ArgumentException("Телефон клиента не соответсвует регулярному выражению");
            this.Phone = Phone;
            this.Name = Name;
        }

    }
}
