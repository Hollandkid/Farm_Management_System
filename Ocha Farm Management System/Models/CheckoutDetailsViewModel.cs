using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class CheckoutDetailsViewModel
    {
        public List<Product> Products { get; set; }
        public ApplicationUser User { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
