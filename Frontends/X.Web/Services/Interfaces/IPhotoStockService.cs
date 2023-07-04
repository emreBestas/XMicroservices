using X.Web.Models.PhotoStocks;

namespace X.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UpLoadphoto(IFormFile photo);
        Task<bool> DeletePhoto(string picUrl);
    }
}
