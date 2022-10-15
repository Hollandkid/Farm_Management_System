using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ocha_Farm_Management_System.Data;
using Ocha_Farm_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FarmController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly CloneRepository _cloneRepository;
        private readonly IHostingEnvironment _hostingEnvironment;   //This is to get the Environment Service

        public FarmController(ApplicationDbContext dbContext, CloneRepository cloneRepository, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _cloneRepository = cloneRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        
        public IActionResult Index()
        {
            //This is to return the List of the Product from the Database...
            var result = _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).ToList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Index(decimal? lowAmount, decimal? highAmount)
        {

            //This is to Search for a Product with a Range... Like a Filter
            var searchRange = _dbContext.Products.Include(g => g.Categories).Include(f => f.Brands)
                .Where(c => c.Price >= lowAmount && c.Price <= highAmount).ToList();
            if (lowAmount == null || highAmount == null)
            {
                searchRange = _dbContext.Products.Include(g => g.Categories).Include(f => f.Brands).ToList();
            }
            return View(searchRange);

        }

        //Get for Create Method
        public IActionResult Create()
        {
            ViewData["farmerid"] = new SelectList(_dbContext.ApplicationUsers.ToList(), "Id", "Firstname");   //This is to populate the ListView from the Database
            return View();
        }

        //Post for Create Method
        [HttpPost]
        public async Task<IActionResult> Create(FarmDetails farmModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                //This is to Check if a product alredy exist in the database
                var searchResult = _dbContext.Products.FirstOrDefault(c => c.Name == farmModel.Name);
                if (searchResult != null)
                {
                    ViewBag.Duplicate = "Farm Name already Exist";
                    ViewData["farmerid"] = new SelectList(_dbContext.ApplicationUsers.ToList(), "Id", "Firstname");
                    return View(farmModel);
                }
                if (image != null)
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    farmModel.Image = "images/" + image.FileName;
                }
                else
                {
                    farmModel.Image = "images/profile.png";
                }
                await _dbContext.Farm.AddAsync(farmModel);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(farmModel);
        }
    }
}
