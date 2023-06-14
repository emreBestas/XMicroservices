using Microsoft.AspNetCore.Mvc;
using X.Shared.ControllerBases;
using X.Shared.DTOs;

namespace X.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceiverPayment()
        {
            return CreateActionResultInstance(Response<NoContentDto>.Success(200));
        }
    }
}
