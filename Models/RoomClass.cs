using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class RoomClass
    {
        //public RoomClass()
        //{
        //    Rooms = new HashSet<Room>();
        //}

        public int IdRoomClass { get; set; }
        public string NameRoomClass { get; set; } = null!;
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
