using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryMvcCoreMysql.Models
{
    public class CheckoutHistory
    {
        public int ID { get; set; }

        [Required]
        public LibraryAsset LibraryAsset { get; set; }

        [Required]
        public LibraryCard LibraryCard { get; set; }

        public DateTime CheckedOut { get; set; }

        public DateTime? CheckedIn { get; set; }
    }
}