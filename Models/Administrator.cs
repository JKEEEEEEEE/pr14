using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Administrator
    {
        //public Administrator()
        //{
        //    Employees = new HashSet<Employee>();
        //    Hotels = new HashSet<Hotel>();
        //}

        public int IdAdministrator { get; set; }
        public string FirstNameAdministrator { get; set; } = null!;
        public string SecondNameAdministrator { get; set; } = null!;
        public string MiddleNameAdministrator { get; set; } = null!;
        public string MailAdministrator { get; set; } = null!;
        public string LoginAdministrator { get; set; } = null!;
        public string PasswordAdministrator { get; set; } = null!;
        public string? Salt { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
        //public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
