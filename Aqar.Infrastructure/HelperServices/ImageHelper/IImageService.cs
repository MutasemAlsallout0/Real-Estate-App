using Microsoft.AspNetCore.Http;

namespace Aqar.Infrastructure.HelperServices.ImageHelper
{
    public interface IImageService
    {
        void DeleteFile(string webRootInnerFolderPath, string fileName);
        Task ResizeImage(string filePath, string uploadedFolder, string fileName);
        Task<string> SaveImage(IFormFile file, string FolderName);
    }
}