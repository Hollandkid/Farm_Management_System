using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class LoginModel 
    {
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
