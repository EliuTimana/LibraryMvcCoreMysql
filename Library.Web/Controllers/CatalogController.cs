using System.Linq;
using LibraryWeb.Models.Catalog;
using LibraryWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryService _libraryService;
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