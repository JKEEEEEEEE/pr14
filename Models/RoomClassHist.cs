using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class RoomClassHist
    {
        public int IdRoomClassHist { get; set; }
        public string NameRoomClassHist { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
