using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Malfunction
    {
        public int IdMalfunctions { get; set; }
        public string PriceMalfunctions { get; set; } = null!;
        public string TypeMalfunctions { get; set; } = null!;
        public string DescriptionMalfunctions { get; set; } = null!;
        public int RoomId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Room Room { get; set; } = null!;
    }
}
