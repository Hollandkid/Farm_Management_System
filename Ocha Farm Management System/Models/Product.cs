using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        //public int Price { get; set; }

        public String Image { get; set; }

        public string ProductColor { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brands { get; set; }

        public int Farm { get; set; }
        public int Farmer { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
