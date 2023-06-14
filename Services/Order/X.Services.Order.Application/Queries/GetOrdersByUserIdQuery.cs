using MediatR;
using X.Services.Order.Application.DTOs;
using X.Shared.DTOs;

namespace X.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }

    }
}
