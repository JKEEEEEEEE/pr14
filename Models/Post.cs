using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class Post
    {
        //public Post()
        //{
        //    Employees = new HashSet<Employee>();
        //}

        public int IdPost { get; set; }
        public string NamePost { get; set; } = null!;
        public string ResponsibilitiesPost { get; set; } = null!;
        public string SalaryPost { get; set; } = null!;
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
