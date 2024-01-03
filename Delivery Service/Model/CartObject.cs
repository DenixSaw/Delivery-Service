using Delivery_Service.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Service.Model {
    public class CartObject : ICartObject {

        private IProduct? _product;
        private int _quantity = 0;
        public IProduct? Product {
            get => _product;
            set { _product = value; }
        }
        public int Quantity {
            get => _quantity;
            set { _quantity = value; }
        }

        public CartObject(IProduct product, int quantity) {
            if (quantity < 0) throw new ArgumentException("Количетсво блюда в корзине должно быть неотрицательным");
            _quantity = quantity;
            _product = product ?? throw new ArgumentException("Блюдо в корзине не может быть пустым");
        }
    }
}
