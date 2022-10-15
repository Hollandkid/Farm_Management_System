using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class FarmViewModel
    {
        public List<FarmDetails> Farms { get; set; }
        public List<Product> Products { get; set; }
    }
}
