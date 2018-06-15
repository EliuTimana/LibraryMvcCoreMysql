using System.Collections.Generic;
using LibraryWeb.Models;

namespace LibraryWeb.Services
{
    public interface ILibraryService
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);
        void Add(LibraryAsset asset);
        string GetAuthorOrDirector(int id);
        string GetDeweyIndex(int id);
        LibraryAssetType GetType(int id);
        string GetTypeLabel(int id);
        string GetTitle(int id);
        string GetIsbn(int id);
        LibraryBranch GetCurrentLocation(int id);
    }
}