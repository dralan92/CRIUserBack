using System;
using System.Collections.Generic;

namespace WebAPI.Models.Cri
{
    public partial class Country
    {
        public Country()
        {
            CriQn = new HashSet<CriQn>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<CriQn> CriQn { get; set; }
    }
}
