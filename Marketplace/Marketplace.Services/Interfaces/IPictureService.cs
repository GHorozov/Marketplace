using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Services.Interfaces
{
    public interface IPictureService
    {
        Task<string> SavePicture(string productId, IFormFile picture, string defaultPicturesPath);
    }
}
