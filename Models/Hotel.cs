using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Hotel
    {
        //public Hotel()
        //{
        //    Excursions = new HashSet<Excursion>();
        //    Floors = new HashSet<Floor>();
        //    Services = new HashSet<Service>();
        //}

        public int IdHotel { get; set; }
        public string NameHotel { get; set; } = null!;
        public string NumberOfFloorsHotel { get; set; } = null!;
        public string NumberOfStarsHotel { get; set; } = null!;
        public string NumberOfRoomsFloorHotel { get; set; } = null!;
        public int AdministratorId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Administrator Administrator { get; set; } = null!;
        //public virtual ICollection<Excursion> Excursions { get; set; }
        //public virtual ICollection<Floor> Floors { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
    }
}
