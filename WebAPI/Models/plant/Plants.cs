using System;
using System.Collections.Generic;

namespace WebAPI.models.plant
{
    public partial class Plants
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public DateTime? LastWateredOn { get; set; }
        public string LastWateredBy { get; set; }
    }
}
