using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Booking
    {
        //public Booking()
        //{
        //    BookingOfServices = new HashSet<BookingOfService>();
        //}

        public int IdBooking { get; set; }
        public string NumberBooking { get; set; } = null!;
        public string DateBooking { get; set; } = null!;
        public string TimeBooking { get; set; } = null!;
        public string PriceBooking { get; set; } = null!;
        public string CheckInDateBooking { get; set; } = null!;
        public string EvictionDateBooking { get; set; } = null!;
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Client Client { get; set; } = null!;
        //public virtual Room Room { get; set; } = null!;
        //public virtual ICollection<BookingOfService> BookingOfServices { get; set; }
    }
}
