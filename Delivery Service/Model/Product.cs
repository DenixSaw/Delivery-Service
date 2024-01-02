using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery_Service.Model.Interfaces;

namespace Delivery_Service.Model
{
    public abstract class Product : IProduct {
        public Guid Id { get; }
        public string Name { get; set; } 
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Product(Guid Id, string Name, string Title, decimal Price) {
            this.Name = Name;
            this.Price = Price;
            this.Title = Title;
            this.Id = Id;
        }

    }
}
