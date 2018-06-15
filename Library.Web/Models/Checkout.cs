using System.ComponentModel.DataAnnotations;
using System;

namespace LibraryMvcCoreMysql.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        [Required]
        public LibraryAsset LibraryAsset { get; set; }

        public LibraryCard LibraryCard { get; set; }

        public DateTime Since { get; set; }
        
        public DateTime Until { get; set; }
    }
}