using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please Enter your First Name")]
        public String Firstname { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter your Last Name")]
        public String Lastname { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter a Valid Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }


        [Required(ErrorMessage = "Please Enter a Valid password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        //public String ConfirmPassword { get; set; }
    }
}
