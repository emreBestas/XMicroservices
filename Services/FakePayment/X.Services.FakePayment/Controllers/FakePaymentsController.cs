using MassTransit;
using Microsoft.AspNetCore.Mvc;
using X.Services.FakePayment.Models;
using X.Shared.ControllerBases;
using X.Shared.DTOs;
using X.Shared.Messages;

namespace X.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task< IActionResult> ReceiverPayment(PaymentDto paymentDto)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));
            var createOrderMessageCommand = new CreateOrderMessageCommand();

            createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessageCommand.Provice = paymentDto.Order.Address.Provice;
            createOrderMessageCommand.District = paymentDto.Order.Address.District;
            createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
            createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;

            paymentDto.Order.OrderItesms.ForEach(x =>
            {
                {
                    createOrderMessageCommand.OrderItesms.Add(new OrderItem
                    {
                        PictureUrl = x.PictureUrl,
                        Price = x.Price,
                        ProdcutId = x.ProdcutId,
                        ProductName = x.ProductName
                    });
                }
            });
            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            return CreateActionResultInstance(Shared.DTOs.Response<NoContentDto>.Success(200));
        }
    }
}
