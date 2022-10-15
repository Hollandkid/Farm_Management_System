using Microsoft.AspNetCore.Mvc;
using Ocha_Farm_Management_System.Data;
using Ocha_Farm_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Areas.Admin.Controllers
{
    public class CloneRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CloneRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Product Clone (Product productModel)
        {
            return _dbContext.Products.Find(productModel.Id);
            
        }
    }
}
