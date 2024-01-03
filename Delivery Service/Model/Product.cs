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
            if (Name.Length == 0) throw new ArgumentException("Название продукта не может быть пустым");
            if (Id == Guid.Empty) throw new ArgumentException("Идентификатор продукта не может быть пустым");
            if (Price <= 0) throw new ArgumentException("Цена продукта должна быть положительным числом");
            this.Name = Name;
            this.Price = Price;
            this.Title = Title;
            this.Id = Id;
        }

    }
}
