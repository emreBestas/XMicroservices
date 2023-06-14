using AutoMapper;
using MongoDB.Driver;
using X.Services.Catalog.DTOs;
using X.Services.Catalog.Models;
using X.Services.Catalog.Settings;
using X.Shared.DTOs;

namespace X.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoCollection.Find(Category => true).ToListAsync();
            var categoryListDto = _mapper.Map<List<CategoryDto>>(categories);
            return Response<List<CategoryDto>>.Success(categoryListDto, 200);
        }
        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        public async Task<Response<CategoryDto>> GetById(string id)
        {
            var category = await _categoCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null) { return Response<CategoryDto>.Fail("Category not found", 404); }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
