using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Floor
    {
        //public Floor()
        //{
        //    Rooms = new HashSet<Room>();
        //}

        public int IdFloor { get; set; }
        public string NumberFloor { get; set; } = null!;
        public string NumberOfRoomsFloor { get; set; } = null!;
        public int HotelId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Hotel Hotel { get; set; } = null!;
        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
