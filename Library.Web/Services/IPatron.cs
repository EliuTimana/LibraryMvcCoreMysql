using System.Collections.Generic;
using LibraryWeb.Models;

namespace LibraryWeb.Services
{
    public interface IPatron
    {
        Patron Get(int id);
        IEnumerable<Patron> GetAll();
        void Add(Patron patron);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        IEnumerable<Holds> GetHolds(int id);
        IEnumerable<Checkout> GetCheckouts(int id);
    }
}