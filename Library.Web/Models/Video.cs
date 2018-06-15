using System.ComponentModel.DataAnnotations;

namespace LibraryMvcCoreMysql.Models
{
    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}