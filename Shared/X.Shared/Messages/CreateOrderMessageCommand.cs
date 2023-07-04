using System;
using System.Collections.Generic;
using System.Text;

namespace X.Shared.Messages
{
    public class CreateOrderMessageCommand
    {
        public CreateOrderMessageCommand()
        {
            OrderItesms = new List<OrderItem>();
        }
        public string BuyerId { get; set; }
        public List<OrderItem> OrderItesms { get; set; }
        public string Provice { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }
    public class OrderItem
    {
        public string ProdcutId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
