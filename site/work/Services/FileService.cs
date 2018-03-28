using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using static System.IO.Directory;
using static System.IO.Path;

namespace QFlow.Services
{
    public class FileService
    {
        public async Task Save(IFormFile file, string path)
        {
            if(!Exists(path))
            {
                CreateDirectory(path);
            }

            await SaveFile(file, path);
        }
        public async Task Save(IList<IFormFile> files, string path)
        {
            if(!Exists(path))
            {
                CreateDirectory(path);
            }

            foreach(var file in files)
            {
                await SaveFile(file, path);
            }
        }

        public IEnumerable<string> Find(string path, string search = "")
        {
            var isExistDirectory = Exists(path);
            var searchName = string.IsNullOrEmpty(search) ? "*" : $"*{search}*";
            return isExistDirectory
                    ? EnumerateFiles(path, searchName, SearchOption.AllDirectories)
                    : Enumerable.Empty<string>();
        }

        public bool HasFile(string file, string path)
        {
            var filePath = Combine(path, file);
            return File.Exists(filePath);
        }

        private async Task SaveFile(IFormFile file, string path)
        {
            if(file.Length <= 0)
                return;

            var filePath = Combine(path, file.FileName);
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
