using MediatR;
using X.Services.Order.Application.Commands;
using X.Services.Order.Application.DTOs;
using X.Services.Order.Domain.OrderAggregate;
using X.Services.Order.Infrastructure;
using X.Shared.DTOs;

namespace X.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;
        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Provice, request.Address.District, request.Address.ZipCode, request.Address.Line, request.Address.Street);
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);
            request.OrderItesms.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProdcutId, x.ProductName, x.PictureUrl, x.Price);
            });
            await _context.Orders.AddAsync(newOrder); await _context.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
