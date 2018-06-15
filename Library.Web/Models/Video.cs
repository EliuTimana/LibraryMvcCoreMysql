using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.Models
{
    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}