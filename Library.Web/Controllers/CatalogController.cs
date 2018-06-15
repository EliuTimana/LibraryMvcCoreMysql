using Microsoft.AspNetCore.Mvc;
using LibraryMvcCoreMysql.Services;
using System.Linq;
using LibraryMvcCoreMysql.Models.Catalog;

namespace LibraryMvcCoreMysql.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryService _libraryService;
        public CatalogController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public IActionResult Index()
        {
            var assetsModels = _libraryService.GetAll()
            .Select(result => new AssetIndexListingModel
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl,
                AuthorOrDirector = _libraryService.GetAuthorOrDirector(result.Id),
                DeweyCallNumber = _libraryService.GetDeweyIndex(result.Id),
                Title = result.Title,
                Type = _libraryService.GetType(result.Id)
            });

            return View(assetsModels);
        }
    }
}