using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Service
    {
        //public Service()
        //{
        //    BookingOfServices = new HashSet<BookingOfService>();
        //}

        public int IdServices { get; set; }
        public string NameServices { get; set; } = null!;
        public string PriceServices { get; set; } = null!;
        public string DescriptionServices { get; set; } = null!;
        public int HotelId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Hotel Hotel { get; set; } = null!;
        //public virtual ICollection<BookingOfService> BookingOfServices { get; set; }
    }
}
