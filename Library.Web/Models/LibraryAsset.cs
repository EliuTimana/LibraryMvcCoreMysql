using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.Models
{
    public enum LibraryAssetType
    {
        Book, Video
    }
    public abstract class LibraryAsset
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public int NumberOfCopies { get; set; }

        public virtual LibraryBranch Location { get; set; }
    }
}