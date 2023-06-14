using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.Services.Order.Application.Commands;
using X.Services.Order.Application.Queries;
using X.Shared.ControllerBases;
using X.Shared.Services;

namespace X.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _identityService;

        public OrdersController(IMediator mediator, ISharedIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response= await _mediator.Send(new GetOrdersByUserIdQuery {UserId=_identityService.GetUserID });
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
    }
}
