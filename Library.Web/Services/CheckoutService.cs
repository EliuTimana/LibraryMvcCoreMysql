using System;
using System.Collections.Generic;
using System.Linq;
using LibraryWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Services
{
    public class CheckoutService : ICheckout
    {
        private readonly LibraryContext _context;

        public CheckoutService(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkout;
        }

        public Checkout GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void Add(Checkout checkout)
        {
            _context.Add(checkout);
            _context.SaveChanges();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
            }

            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);
            UpdateAssetStatus(assetId, "Checked Out");

            var libraryCard = _context.LibraryCards
            .Include(c => c.Checkouts)
            .FirstOrDefault(c => c.Id == libraryCardId);

            var checkout = new Checkout
            {
                LibraryAsset = item,
                LibraryCard = libraryCard,
                Since = DateTime.Now,
                Until = GetDefaultCheckoutTime(DateTime.Now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = DateTime.Now,
                LibraryAsset = item,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int assetId)
        {
            var isCheckedOut = _context.Checkout
            .Where(c => c.LibraryAsset.Id == assetId)
            .Any();

            return isCheckedOut;
        }

        public void CheckInItem(int assetId)
        {
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);

            _context.Update(item);

            RemoveExistingCheckouts(assetId);
            CloseExistingCheckoutHistory(assetId);

            var currentHolds = _context.Holds
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == assetId);

            if (currentHolds.Any())
            {
                CheckoutToEarliestHold(assetId, currentHolds);
                return;
            }

            UpdateAssetStatus(assetId, "Available");

            _context.SaveChanges();
        }

        private void CheckoutToEarliestHold(int assetId, IQueryable<Holds> currentHolds)
        {
            var earliestHold = currentHolds
                .OrderBy(h => h.HoldedPlaced)
                .FirstOrDefault();

            if (earliestHold == null) return;

            var card = earliestHold.LibraryCard;

            _context.Remove(earliestHold);
            _context.SaveChanges();

            CheckOutItem(assetId, card.Id);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            return _context.CheckoutHistory
                .Include(h => h.LibraryAsset)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            var asset = _context.LibraryAssets
            .Include(a => a.Status)
            .FirstOrDefault(a => a.Id == assetId);

            var card = _context.LibraryCards.FirstOrDefault(c => c.Id == libraryCardId);

            if (asset.Status.Name == "Available")
            {
                UpdateAssetStatus(assetId, "On Hold");
            }

            var hold = new Holds
            {
                HoldedPlaced = DateTime.Now,
                LibraryAsset = asset,
                LibraryCard = card
            };

            _context.Add(hold);
            _context.SaveChanges();
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _context.Holds
            .Include(h => h.LibraryAsset)
            .Include(h => h.LibraryCard)
            .FirstOrDefault(h => h.Id == holdId);

            var cardId = hold?.LibraryCard.Id;

            var patron = _context.Patrons
            .Include(p => p.LibraryCard)
            .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;

        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return _context.Holds
            .Include(h => h.LibraryAsset)
            .Include(h => h.LibraryCard)
            .FirstOrDefault(h => h.Id == id)
            .HoldedPlaced;
        }

        public string GetCurrentCheckoutPatron(int id)
        {
            var checkout = GetCheckoutByAssetId(id);

            if (checkout == null)
            {
                return "";
            }

            var cardId = checkout.LibraryCard.Id;

            var patron = _context.Patrons
            .Include(p => p.LibraryCard)
            .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;
        }

        private Checkout GetCheckoutByAssetId(int id)
        {
            return _context.Checkout
            .Include(c => c.LibraryAsset)
            .Include(c => c.LibraryCard)
            .FirstOrDefault(c => c.LibraryAsset.Id == id);
        }

        public IEnumerable<Holds> GetCurrentHolds(int id)
        {
            return _context.Holds.Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAsset.Id == id);
        }

        public Checkout GetLatestCheckout(int id)
        {
            return _context.Checkout
                .Where(c => c.LibraryAsset.Id == id)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, "Lost");

            _context.SaveChanges();
        }

        public void MarkFound(int assetId)
        {
            UpdateAssetStatus(assetId, "Avaliable");
            RemoveExistingCheckouts(assetId);
            CloseExistingCheckoutHistory(assetId);

            _context.SaveChanges();
        }

        private void CloseExistingCheckoutHistory(int assetId)
        {
            //close any existing checkout history
            var history = _context.CheckoutHistory
                .Include(c=>c.LibraryAsset)
                .Include(c=>c.LibraryCard)
                .FirstOrDefault(h => h.LibraryAsset.Id == assetId && h.CheckedIn == null);

            if (history == null) return;

            _context.Update(history);
            history.CheckedIn = DateTime.Now;
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            //remove any existing checkouts on the item
            var checkout = _context.Checkout
                .FirstOrDefault(c => c.LibraryAsset.Id == assetId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        private void UpdateAssetStatus(int assetId, string status)
        {
            var item = _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);

            if (item == null)
            {
                return;
            }

            _context.Update(item);

            item.Status = _context.Statuses.FirstOrDefault(s => s.Name == status);
        }
    }
}