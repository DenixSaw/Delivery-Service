using Delivery_Service.Model;
using Delivery_Service.Model.Interfaces;
using Delivery_Service.Utils;
using System;
using System.Collections.Generic;

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

        public List<Tuple<IProduct, int>> Products { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public  OrderStatus OrderStatus { get; set; }

        public Order(Guid Id, DateTime DateOfAdding, List<Tuple<IProduct, int>> Products, string Address, IClient Client, string Comment, PaymentMethod paymentMethod, Guid Courier, OrderStatus OrderStatus) {
            this.Id = Id;
            this.DateOfAdding = DateOfAdding;
            this.Products = Products;
            this.Address = Address;
            this.Comment = Comment;
            this.PaymentMethod = paymentMethod;
            this.Courier = Courier;
            this.Client = Client;
            this.OrderStatus = OrderStatus;
        }
    }
}
