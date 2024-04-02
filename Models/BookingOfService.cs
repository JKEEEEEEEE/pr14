using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class BookingOfService
    {
        public int IdBookingOfServices { get; set; }
        public string ReservationDateBookingOfServices { get; set; } = null!;
        public int ServicesId { get; set; }
        public int BookingId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Booking Booking { get; set; } = null!;
        //public virtual Service Services { get; set; } = null!;
    }
}
