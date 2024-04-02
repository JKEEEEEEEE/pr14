using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Room
    {
        //public Room()
        //{
        //    Bookings = new HashSet<Booking>();
        //    Malfunctions = new HashSet<Malfunction>();
        //}

        public int IdRoom { get; set; }
        public string PriceRoom { get; set; } = null!;
        public int FloorId { get; set; }
        public int RoomCleaningId { get; set; }
        public int RoomClassId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Floor Floor { get; set; } = null!;
        //public virtual RoomClass RoomClass { get; set; } = null!;
        //public virtual RoomCleaning RoomCleaning { get; set; } = null!;
        //public virtual ICollection<Booking> Bookings { get; set; }
        //public virtual ICollection<Malfunction> Malfunctions { get; set; }
    }
}
