using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery_Service.Model.Interfaces;

namespace Delivery_Service.Model {
    public class Dish : Product, IDish {
        public int Weight { get; set; }

        public Dish(Guid Id, string Name, string Title, int Weight, decimal Price) : base(Id, Name, Title, Price) {
            if (Weight <= 0) throw new ArgumentException("Вес блюда должен быть положительным целым числом");
            this.Weight = Weight;
        }
    }
}
