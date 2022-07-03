using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var result = NewPath(file);

            var sourcepath = Path.GetTempFileName();

            using (var stream = new FileStream(sourcepath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            File.Move(sourcepath, result.newPath);

            return result.Path2;
        }

        public static string Update(string sourcePath, IFormFile file)
        {
            var result = NewPath(file);
            using (var stream = new FileStream(result.newPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            File.Delete(sourcePath);

            return result.Path2;
        }

        public static IResult Delete(string path)
        {
            File.Delete(path);
            return new SuccessResult();
        }

        public static (string newPath, string Path2) NewPath(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;

            var newPath = Guid.NewGuid() + fileExtension;

            string path = Environment.CurrentDirectory + @"\wwwroot\Images";

            string result = $@"{path}\{newPath}";

            return (result, $"\\Images\\{newPath}");
        }
    }
}
