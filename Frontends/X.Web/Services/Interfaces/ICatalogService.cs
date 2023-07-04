using X.Web.Models.Catalogs;

namespace X.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetByCourseId(string courseId);
        Task<bool> CreateCourseAsync(CourseCreateInputViewModel courseCreateInput );
        Task<bool> UpdateCourseAsync(CourseUpdateViewModel courseUpdateInput );
        Task<bool> DeleteCourseAsync(string courseId);
    }
}
