using System;

namespace LibraryMvcCoreMysql.Models
{
    public class Holds
    {
        public int Id { get; set; }
        public virtual LibraryAsset LibraryAsset { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
        public DateTime HoldedPlaced { get; set; }
    }
}