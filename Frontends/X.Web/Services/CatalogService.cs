using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Net.Http.Json;
using X.Shared.DTOs;
using X.Web.Helpers;
using X.Web.Models;
using X.Web.Models.Catalogs;
using X.Web.Services.Interfaces;

namespace X.Web.Services
{
    public class CatalogService : ICatalogService
    { 
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInputViewModel courseCreateInput)
        {
            var resultPhoto = await _photoStockService.UpLoadphoto(courseCreateInput.PhotoFormFile);
            if (resultPhoto != null) { courseCreateInput.Picture = resultPhoto.Url; }

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInputViewModel>("courses", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode) { return null; }
            var responseSuccses = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseSuccses.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if(!response.IsSuccessStatusCode) { return null; }
            var responseSuccses = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccses.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccses.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode) { return new List<CourseViewModel>(); }
            var responseSuccses = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccses.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccses.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode) { return null; }
            var responseSuccses = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            responseSuccses.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccses.Data.Picture);
            return responseSuccses.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateViewModel courseUpdateInput)
        {
            var resultPhoto = await _photoStockService.UpLoadphoto(courseUpdateInput.PhotoFormFile);
            if (resultPhoto != null) {await _photoStockService.DeletePhoto(courseUpdateInput.Picture); courseUpdateInput.Picture = resultPhoto.Url; }
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateViewModel>("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
