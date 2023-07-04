using X.Shared.DTOs;
using X.Web.Models.PhotoStocks;
using X.Web.Services.Interfaces;

namespace X.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string picUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={picUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UpLoadphoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0) { return null; }
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";
            using var ms = new MemoryStream();
            await photo.CopyToAsync(ms);
            var multiPartContent = new MultipartFormDataContent();
            multiPartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFilename);
            var response = await _httpClient.PostAsync("photos", multiPartContent);
            if (!response.IsSuccessStatusCode) { return null; }
            var responseSuccess= await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();
            return responseSuccess.Data;
        }
    }
}
