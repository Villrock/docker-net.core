using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QFlow.Extensions;
using QFlow.Helper;
using QFlow.Models.DocumentViewModels;
using QFlow.Services;

namespace QFlow.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SettingsService _settings;
        private readonly FileService _fileService;

        public DocumentController(IHostingEnvironment environment, SettingsService settings, FileService fileService)
        {
            _hostingEnvironment = environment;
            _settings = settings;
            _fileService = fileService;
        }

        public IActionResult Index(int page = 1)
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var brocuresFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, Consts.BROCHURE_FOLDER_NAME);

            var findFiles = _fileService.Find(brocuresFolderPath);

            var model = new DocumentVaultViewModel
            {
                Documents = from file in findFiles.Skip(startIndex).Take(_settings.PageSize)
                            select new DocumentViewModel
                            {
                                Name = Path.GetFileName(file),
                                Path = "/" + Consts.BROCHURE_FOLDER_NAME + "/" + Path.GetFileName(file)
                            },
                PagingInfo = _settings.GetPagingInfo(page, startIndex, findFiles.Count(), Request.ContentUrl())
            };
            return View(model);
        }

        public JsonResult List(int page = 1, string search = "")
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var brocuresFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, Consts.BROCHURE_FOLDER_NAME);

            var findFiles = _fileService.Find(brocuresFolderPath, search);

            var model = new DocumentVaultViewModel
            {
                Documents = from file in findFiles.Skip(startIndex).Take(_settings.PageSize)
                    select new DocumentViewModel
                    {
                        Name = Path.GetFileName(file),
                        Path = "/" + Consts.BROCHURE_FOLDER_NAME + "/" + Path.GetFileName(file)
                    },
                PagingInfo = _settings.GetPagingInfo(page, startIndex, findFiles.Count(), Request.ContentUrl())
            };
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IList<IFormFile> files)
        {
            var brocuresFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, Consts.BROCHURE_FOLDER_NAME);

            await _fileService.Save(files, brocuresFolderPath);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Download(string filename, string folder = "")
        {
            if(filename == null)
                return Content("filename not present");

            folder = string.IsNullOrEmpty(folder) ? Consts.BROCHURE_FOLDER_NAME : folder;
            var path = Path.Combine(_hostingEnvironment.WebRootPath, folder, filename);
            if(!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            var memory = new MemoryStream();
            using(var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        [HttpGet]
        public JsonResult GetList(string search)
        {
            var brocuresFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, Consts.BROCHURE_FOLDER_NAME);

            var findFiles = _fileService.Find(brocuresFolderPath, search);

            return Json(from file in findFiles
                        select new
                        {
                            Name = Path.GetFileName(file)
                        });
        }

        #region Implimentation routes

        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        #endregion
    }
}