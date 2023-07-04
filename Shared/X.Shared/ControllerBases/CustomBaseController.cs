using Microsoft.AspNetCore.Mvc;
using X.Shared.DTOs;

namespace X.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}
