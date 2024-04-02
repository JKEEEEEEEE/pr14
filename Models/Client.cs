using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Client
    {
        //public Client()
        //{
        //    Bookings = new HashSet<Booking>();
        //}

        public int IdClient { get; set; }
        public string FirstNameClient { get; set; } = null!;
        public string SecondNameClient { get; set; } = null!;
        public string MiddleNameClient { get; set; } = null!;
        public string MailClient { get; set; } = null!;
        public string LoginClient { get; set; } = null!;
        public string PasswordClient { get; set; } = null!;
        public string? Salt { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Booking> Bookings { get; set; }
    }
}
