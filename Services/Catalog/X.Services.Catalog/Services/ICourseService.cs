using X.Services.Catalog.DTOs;
using X.Shared.DTOs;

namespace X.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetById(string id);
        Task<Response<List<CourseDto>>> GetByUserId(string userid);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContentDto>> DeleteAsync(string id);
    }
}
