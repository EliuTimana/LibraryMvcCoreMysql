using System;
using System.Linq;
using LibraryWeb.Models.Checkouts;
using LibraryWeb.Models.Catalog;
using LibraryWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly ICheckout _checkoutService;

        public CatalogController(ILibraryService libraryService, ICheckout checkoutService)
        {
            _libraryService = libraryService;
            _checkoutService = checkoutService;
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

        public IActionResult Detail(int id)
        {
            var asset = _libraryService.GetById(id);

            var model = new AssetDetailModel
            {
                Id = asset.Id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Type = _libraryService.GetTypeLabel(id),
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _libraryService.GetAuthorOrDirector(id),
                CurrentLocation = _libraryService.GetCurrentLocation(id).Name,
                DeweyCallNumber = _libraryService.GetDeweyIndex(id),
                Isbn = _libraryService.GetIsbn(id),
                PatronName = _checkoutService.GetCurrentCheckoutPatron(id),
                CheckoutHistory = _checkoutService.GetCheckoutHistory(id),
                CurrentHolds = _checkoutService.GetCurrentHolds(id).Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkoutService.GetCurrentHoldPlaced(a.Id),
                    PatronName = _checkoutService.GetCurrentHoldPatronName(a.Id)
                })
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var asset = _libraryService.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = asset.Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id)
            };

            return View(model);
        }
        public IActionResult Checkin(int id)
        {
            var asset = _libraryService.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = asset.Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = _libraryService.GetById(id);

            var model = new CheckoutModel
            {
                AssetId = asset.Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id),
                HoldCount = _checkoutService.GetCurrentHolds(id).Count()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkoutService.CheckOutItem(assetId, libraryCardId);

            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckin(int assetId)
        {
            _checkoutService.CheckInItem(assetId);

            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkoutService.PlaceHold(assetId, libraryCardId);

            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult MarkLost(int assetId)
        {
            _checkoutService.MarkLost(assetId);

            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult MarkFound(int assetId)
        {
            _checkoutService.MarkFound(assetId);

            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}