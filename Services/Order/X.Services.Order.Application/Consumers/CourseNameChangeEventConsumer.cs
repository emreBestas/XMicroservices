using MassTransit;
using Microsoft.EntityFrameworkCore;
using X.Services.Order.Infrastructure;
using X.Shared.Messages;

namespace X.Services.Order.Application.Consumers
{
    public class CourseNameChangeEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangeEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems= await _orderDbContext.OrderItems.Where(x=>x.ProdcutId==context.Message.CourseId).ToListAsync();
            orderItems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.UpdateName, x.PictureUrl, x.Price);
            });
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
