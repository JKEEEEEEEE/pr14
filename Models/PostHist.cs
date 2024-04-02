using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class PostHist
    {
        public int IdPostHist { get; set; }
        public string NamePostHist { get; set; } = null!;
        public string ResponsibilitiesPostHist { get; set; } = null!;
        public int SalaryPostHist { get; set; }
    }
}
