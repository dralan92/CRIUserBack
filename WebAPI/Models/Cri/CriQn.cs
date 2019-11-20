using System;
using System.Collections.Generic;

namespace WebAPI.Models.Cri
{
    public partial class CriQn
    {
        public int QnId { get; set; }
        public string Qn { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Opt1 { get; set; }
        public int Weight1 { get; set; }
        public string Opt2 { get; set; }
        public int Weight2 { get; set; }
        public string Opt3 { get; set; }
        public int? Weight3 { get; set; }
        public string Opt4 { get; set; }
        public int? Weight4 { get; set; }
        public int CountryFk { get; set; }
        public int TierFk { get; set; }

        public Country CountryFkNavigation { get; set; }
        public Tier TierFkNavigation { get; set; }
    }
}
