using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace QFlow.Models.HomeViewModals
{
    public class FileUploadViewModel
    {
        public IEnumerable<IFormFile> Files { get; set; }
        public IEnumerable<string> FilePaths{ get; set; }
    }
}
