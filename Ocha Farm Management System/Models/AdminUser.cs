using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class AdminUser :BaseEntityModel
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
       // public String Email { get; set; }
        public String Role { get; set; }

    }
}
