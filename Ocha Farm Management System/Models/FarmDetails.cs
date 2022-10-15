using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class FarmDetails
    {

        public string Name { get; set; }
        public int Id { get; set; }
        public int FarmerId { get; set; }

        public String Farmer { get; set; }

        public String Image { get; set; }

        public string Address { get; set; }
        public string State { get; set; }
        public bool IsAvailable { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
