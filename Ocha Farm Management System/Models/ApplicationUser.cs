using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Password { get; set; }
        //public String ConfirmPassword { get; set; }
        public String Address { get; set; }
        public String Role { get; set; }
        public String Country { get; set; }
        public String State { get; set; }
        public String zip { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public DateTime TimeUpdated { get; set; } = DateTime.Now;
    }
}
