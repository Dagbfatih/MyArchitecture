using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {

        private PathInfo CreateFullPath(IFormFile formFile, string uploadedDirectory)
        {
            if (!Directory.Exists(uploadedDirectory))
            {
                Directory.CreateDirectory(uploadedDirectory);
            }

            string fileName = CreateGuidFileName(uploadedDirectory, Path.GetExtension(formFile.FileName));
            string fullPath = Path.Combine(uploadedDirectory, fileName);

            return new PathInfo { FileName = fileName, FullPath = fullPath };
        }

        public async Task<string> Add(IFormFile formFile, string uploadedDirectory, string uploadedFolderName)
        {
            var fullPathInfo = CreateFullPath(formFile, uploadedDirectory);
            using (var stream = new FileStream(fullPathInfo.FullPath, FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            return $"/{uploadedFolderName}/{fullPathInfo.FileName}";
        }

        public Task<string> Update(IFormFile formFile, string uploadedDirectory, string uploadedFolderName, string oldPath)
        {
            File.Delete(oldPath);
            return Add(formFile, uploadedDirectory, uploadedFolderName);
        }

        private string CreateGuidFileName(string uploadedDirectory, string fileExtension)
        {
            string fileName;
            string fullPath;

            //checking file name if exists create repeatedly
            do
            {
                fileName = $@"{Guid.NewGuid()}{fileExtension}";
                fullPath = Path.Combine(uploadedDirectory, fileName);
            } while (File.Exists(fullPath));

            return fileName;
        }

        public string GetDefaultImageByFileName(string fileName)
        {
            var defaultPath = "/DefaultImages/" + fileName;
            return defaultPath;
        }

        public void Delete(string oldPath)
        {
            File.Delete(oldPath);
        }
    }
}
