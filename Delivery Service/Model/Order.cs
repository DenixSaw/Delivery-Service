using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Utils;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Delivery_Service.Entities {

    public enum PaymentMethod {
        PaymentByCard,
        CashPayment,
        PaymentByCardToCourier
    }

    public enum OrderStatus {
        New,
        Delivered
    }

    public class Order : IOrder {
        public Guid Id { get; }

        public DateTime DateOfAdding { get; }

        public decimal TotalCost { get; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public Guid Courier { get; set; }

        public IClient Client { get; set; }

        public IList<ICartObject> Products { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public Order(Guid Id, DateTime DateOfAdding, IList<ICartObject> Products, string Address, IClient Client, string Comment, PaymentMethod PaymentMethod, Guid Courier, OrderStatus OrderStatus, decimal TotalCost) {
            if (Id == Guid.Empty) throw new ArgumentException("Идентификатор заказа не может быть пустым");
            if (DateOfAdding == DateTime.MinValue) throw new ArgumentException("Некорректная дата добавления заказа");
            if (Address  == null || Address.Length == 0) throw new ArgumentException("Поле адрес у заказа не может быть пустым");
            if (Products == null || Products.Count == 0) throw new ArgumentException("Количество продуктов в заказе не может быть равным нулю");
            if (TotalCost <= 0) throw new ArgumentException("Цена заказа не может быть отрицательной или равной нулю");
            this.Id = Id;
            this.DateOfAdding = DateOfAdding;
            this.Products = Products;
            this.Address = Address;
            this.Comment = Comment;
            this.PaymentMethod = PaymentMethod;
            this.Courier = Courier;
            this.Client = Client;
            this.OrderStatus = OrderStatus;
            this.TotalCost = TotalCost;
        }
    }
}
