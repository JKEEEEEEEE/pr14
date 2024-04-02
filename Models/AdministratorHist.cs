using System;
using System.Collections.Generic;

namespace Hotels.Models
{
    public partial class AdministratorHist
    {
        public int IdAdministratorHist { get; set; }
        public string FirstNameAdministratorHist { get; set; } = null!;
        public string SecondNameAdministratorHist { get; set; } = null!;
        public string MiddleNameAdministratorHist { get; set; } = null!;
        public string MailAdministratorHist { get; set; } = null!;
        public string LoginAdministratorHist { get; set; } = null!;
        public string PasswordAdministratorHist { get; set; } = null!;
        public string? SaltHist { get; set; }
        public bool IsDeletedHist { get; set; }
    }
}
