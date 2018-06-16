using System;
using System.Collections.Generic;
using LibraryWeb.Models;

namespace LibraryWeb.Services
{
    public interface ICheckout
    {
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int id);
        void Add(Checkout checkout);
        void CheckOutItem(int assetId, int libraryCardId);
        void CheckInItem(int assetId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int id);
        Checkout GetLatestCheckout(int id);
        string GetCurrentCheckoutPatron(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
        string GetCurrentHoldPatronName(int id);
        bool IsCheckedOut(int id);
        DateTime GetCurrentHoldPlaced(int id);
        IEnumerable<Holds> GetCurrentHolds(int id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);
    }
}