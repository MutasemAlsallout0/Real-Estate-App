using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Infrastructure.HelperServices.ImageHelper
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveImage(IFormFile file, string FolderName)
        {
            string uniqueFileName = null;
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FolderName);
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                await ResizeImage(filePath, uploadsFolder, uniqueFileName);
            }


            return uniqueFileName;

        }

        public async Task ResizeImage(string filePath, string uploadedFolder, string fileName)
        {
            var folderMedi = Path.Combine(uploadedFolder, "Thumbs", "Med", fileName);
            var folderSmall = Path.Combine(uploadedFolder, "Thumbs", "Small", fileName);

            using (Image input = Image.Load(filePath))
            {

                input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Crop, Size = new Size(400, 400) }));
                await input.SaveAsync(folderMedi);

                input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Crop, Size = new Size(120, 120) }));
                await input.SaveAsync(folderSmall);

            }

        }

        public void DeleteFile(string webRootInnerFolderPath, string fileName)
        {
            string fileUrl = Path.Combine(_webHostEnvironment.WebRootPath, webRootInnerFolderPath, fileName);

            File.Delete(fileUrl);

        }

    }
}