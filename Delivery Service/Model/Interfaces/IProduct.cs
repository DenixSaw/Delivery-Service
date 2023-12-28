using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Delivery_Service.Model.Interfaces
{
    [JsonDerivedType(typeof(Dish))]
    public interface IProduct {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

    }
}
