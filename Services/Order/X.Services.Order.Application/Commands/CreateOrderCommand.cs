using MediatR;
using X.Services.Order.Application.DTOs;
using X.Shared.DTOs;

namespace X.Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItesms { get; set; }
        public AddressDto AddressDto { get; set; }

    }
}
