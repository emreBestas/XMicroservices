using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.Services.Order.Domain.OrderAggregate;

namespace X.Services.Order.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get;  set; }
        public AddressDto Adress { get;  set; }
        public string BuyerId { get;  set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
