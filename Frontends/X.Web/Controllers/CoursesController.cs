using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.Shared.Services;
using X.Web.Models.Catalogs;
using X.Web.Services.Interfaces;

namespace X.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedidentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedidentityService)
        {
            _catalogService = catalogService;
            _sharedidentityService = sharedidentityService;
        }

        public async Task< IActionResult> Index()
        {
            return View( await _catalogService.GetAllCourseByUserIdAsync(_sharedidentityService.GetUserID));
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInputViewModel courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            courseCreateInput.UserId = _sharedidentityService.GetUserID;
            await _catalogService.CreateCourseAsync(courseCreateInput);
           return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course= await _catalogService.GetByCourseId(id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            if (course == null) { RedirectToAction(nameof(Index)); }
            CourseUpdateViewModel courseUpdate = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = course.Feature,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };
            return View(courseUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateViewModel courseUpdate )
        {
            //var course = await _catalogService.GetByCourseId(courseUpdate.Id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdate.Id);
            if (!ModelState.IsValid)
            {
               return View();
            }
            await _catalogService.UpdateCourseAsync(courseUpdate);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
