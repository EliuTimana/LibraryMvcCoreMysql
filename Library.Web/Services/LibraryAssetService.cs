using System.Collections.Generic;
using System.Linq;
using LibraryMvcCoreMysql.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMvcCoreMysql.Services
{
    public class LibraryAssetService : ILibraryService
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(LibraryAsset asset)
        {
            _context.Add(asset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
            .Include(asset => asset.Status)
            .Include(asset => asset.Location);
        }

        public string GetAuthorOrDirector(int id)
        {
            if (GetType(id) == LibraryAssetType.Book)
            {
                return _context.Books.FirstOrDefault(b => b.Id == id).Author;
            }
            else if (GetType(id) == LibraryAssetType.Video)
            {
                return _context.Videos.FirstOrDefault(v => v.Id == id).Director;
            }

            return "Unkwon";

            /*
            var isBook = _context.LibraryAssets.OfType<Book>().Where(b => b.Id == id).Any();
            var isVideo = _context.LibraryAssets.OfType<Video>().Where(v => v.Id == id).Any();

            return isBook ?
            _context.Books.FirstOrDefault(b => b.Id == id).Author :
            _context.Videos.FirstOrDefault(v => v.Id == id).Director ??
            "Unkwon";
             */
        }

        public LibraryAsset GetById(int id)
        {
            return GetAll().FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            // otra forma
            // var isBook = _context.LibraryAssets.OfType<Book>().Where(b => b.Id == id).Any();
            if (GetType(id) == LibraryAssetType.Book)
            {
                return _context.Books.FirstOrDefault(b => b.Id == id).DeweyIndex;
            }

            return "";
        }

        public string GetIsbn(int id)
        {
            if (GetType(id) == LibraryAssetType.Book)
            {
                return _context.Books.FirstOrDefault(b => b.Id == id).ISBN;
            }

            return "";
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(a => a.Id == id).Title;
        }

        public LibraryAssetType GetType(int id)
        {
            return _context.LibraryAssets.OfType<Book>().Where(b => b.Id == id).Any() ? LibraryAssetType.Book : LibraryAssetType.Video;
        }
    }
}