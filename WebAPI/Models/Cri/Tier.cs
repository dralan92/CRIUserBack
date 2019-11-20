using System;
using System.Collections.Generic;

namespace WebAPI.Models.Cri
{
    public partial class Tier
    {
        public Tier()
        {
            CriQn = new HashSet<CriQn>();
        }

        public int TierId { get; set; }
        public string TierName { get; set; }

        public ICollection<CriQn> CriQn { get; set; }
    }
}
