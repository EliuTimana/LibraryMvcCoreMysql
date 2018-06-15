using System.Collections.Generic;

namespace LibraryMvcCoreMysql.Models.Catalog
{
    public class AssetIndexModel
    {
        public IEnumerable<AssetIndexListingModel> Assets { get; set; }
    }
}