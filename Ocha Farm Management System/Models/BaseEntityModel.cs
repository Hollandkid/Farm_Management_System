using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class BaseEntityModel
    {
        public string UniqueId { get; set; }
        public long Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
