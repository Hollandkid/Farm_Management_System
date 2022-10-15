using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Models
{
    public class ProductViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public List<Category> ProductTypes { get; set; }
    }
}
