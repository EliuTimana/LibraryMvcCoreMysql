using System.Collections.Generic;

namespace LibraryWeb.Models.PatronModel
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronDetailModel> Patrons { get; set; }
    }
}