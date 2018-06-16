using System.Collections.Generic;
using System.Linq;
using LibraryWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Services
{
    public class PatronService : IPatron
    {
        private readonly LibraryContext _context;
        public PatronService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(Patron patron)
        {
            _context.Add(patron);
            _context.SaveChanges();
        }

        public Patron Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
            .Include(p => p.LibraryCard)
            .Include(p => p.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id)
        {
            var cardId = Get(id).LibraryCard.Id;

            return _context.CheckoutHistory
            .Include(c => c.LibraryCard)
            .Include(c => c.LibraryAsset)
            .Where(c => c.LibraryCard.Id == cardId)
            .OrderByDescending(c => c.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int id)
        {
            var cardId = Get(id).LibraryCard.Id;

            return _context.Checkout
            .Include(p => p.LibraryCard)
            .Include(p => p.LibraryAsset)
            .Where(c => c.LibraryCard.Id == cardId);
        }

        public IEnumerable<Holds> GetHolds(int id)
        {
            var cardId = Get(id).LibraryCard.Id;

            return _context.Holds
            .Include(p => p.LibraryCard)
            .Include(p => p.LibraryAsset)
            .Where(p => p.LibraryCard.Id == cardId)
            .OrderByDescending(p => p.HoldedPlaced);
        }
    }
}