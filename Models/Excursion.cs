using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Excursion
    {
        public int IdExcursion { get; set; }
        public string RouteExcursion { get; set; } = null!;
        public string DateExcursion { get; set; } = null!;
        public string TimeExcursion { get; set; } = null!;
        public string SecondNameGuideExcursion { get; set; } = null!;
        public int HotelId { get; set; }
        public bool IsDeleted { get; set; }

        public Hotel Hotel { get; set; } = null!;
    }
}
