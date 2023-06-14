using X.Services.Catalog.DTOs;
using X.Services.Catalog.Models;
using X.Shared.DTOs;

namespace X.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetById(string id);

    }
}
