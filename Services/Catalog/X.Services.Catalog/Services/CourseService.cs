using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using X.Services.Catalog.DTOs;
using X.Services.Catalog.Models;
using X.Services.Catalog.Settings;
using X.Shared.DTOs;
using ZstdSharp.Unsafe;

namespace X.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;


        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(Course => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else { courses = new List<Course>(); }

            var courseListDto = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(courseListDto, 200);
        }

        public async Task<Response<CourseDto>> GetById(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null) { return Response<CourseDto>.Fail("Course not found", 404); }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }
        public async Task<Response<List<CourseDto>>> GetByUserId(string userid)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userid).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else { courses = new List<Course>(); }

            var courseListDto = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(courseListDto, 200);
        }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        public async Task<Response<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCource = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCource);
            if (result == null) { Response<NoContentDto>.Fail("Course not found", 404); }
            return Response<NoContentDto>.Success(204);
        }
        public async Task<Response<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContentDto>.Success(204);
            }
            else
            {
                return Response<NoContentDto>.Fail("Course not found", 404);
            }
        }
    }
}