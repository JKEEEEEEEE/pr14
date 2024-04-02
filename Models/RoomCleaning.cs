using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class RoomCleaning
    {
        //public RoomCleaning()
        //{
        //    Rooms = new HashSet<Room>();
        //}

        public int IdRoomCleaning { get; set; }
        public string DateRoomCleaning { get; set; } = null!;
        public string DateOfChangeOfBedLinenRoomCleaning { get; set; } = null!;
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
