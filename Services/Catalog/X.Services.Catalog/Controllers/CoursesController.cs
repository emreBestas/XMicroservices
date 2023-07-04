using Microsoft.AspNetCore.Mvc;
using X.Services.Catalog.DTOs;
using X.Services.Catalog.Services;
using X.Shared.ControllerBases;

namespace X.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetById(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userid}")]
        public async Task<IActionResult> GetAllByUserId(string userid)
        {
            var response = await _courseService.GetByUserId(userid);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdate)
        {
            var response = await _courseService.UpdateAsync(courseUpdate);
            return CreateActionResultInstance(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
