
using System.Collections.Generic;
using System.Linq;
using LibraryWeb.Models;
using LibraryWeb.Models.PatronModel;
using LibraryWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatron _patronService;
        public PatronController(IPatron patronService)
        {
            _patronService = patronService;
        }
        public IActionResult Index()
        {
            var patrons = _patronService.GetAll();

            var patronModels = patrons.Select(p => new PatronDetailModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                Address = p.Address,
                MemberSince = p.LibraryCard.Created,
                Telephone = p.TelephoneNumber,
                HomeLibraryBranch = p.HomeLibraryBranch.Name,
                OverdueFees = p.LibraryCard.Fees,
            }).ToList();

            var model = new PatronIndexModel
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var patron = _patronService.Get(id);

            var patronModel = new PatronDetailModel
            {
                Id = patron.Id,
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                LibraryCardId = patron.LibraryCard.Id,
                Address = patron.Address,
                MemberSince = patron.LibraryCard.Created,
                Telephone = patron.TelephoneNumber,
                HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                OverdueFees = patron.LibraryCard.Fees,
                AssetsCheckedOut = _patronService.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _patronService.GetCheckoutHistory(id),
                Holds = _patronService.GetHolds(id)
            };

            return View(patronModel);
        }
    }
}