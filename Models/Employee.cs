using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Employee
    {
        public int IdEmployee { get; set; }
        public string FirstNameEmployee { get; set; } = null!;
        public string SecondNameEmployee { get; set; } = null!;
        public string MiddleNameEmployee { get; set; } = null!;
        public int PostId { get; set; }
        public int AdministratorId { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual Administrator Administrator { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
