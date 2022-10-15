using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ocha_Farm_Management_System.Data;
using Ocha_Farm_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            var data = _dbContext.Categories.ToList();
            return View(data);
        }


        //Get Create Action Method
        public IActionResult Create()
        {

            
            return View();
        }


        //Post Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category productTypes)
        {
            if (ModelState.IsValid)
            {

                //Add to Db
                _dbContext.Categories.Add(productTypes);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Product Type has been Saved";
                return RedirectToAction("Index");
            }

            return View(productTypes);
        }

        //Get Edit Action Method
        public IActionResult Edit(int? Id)
        {
            if (Id==null)
            {
                return NotFound();
            }

            var productType = _dbContext.Categories.Find(Id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }


        //Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category productTypes)
        {
            if (ModelState.IsValid)
            {

                //Uodate Db
                _dbContext.Categories.Update(productTypes);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Product Type Updated";

                return RedirectToAction("Index");
            }

            return View(productTypes);
        }


        //Get Details Action Method
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var productType = _dbContext.Categories.Find(Id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }


        //Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Category productTypes)
        {
            TempData["save"] = "";

            return RedirectToAction("Index");
        }


        //Get Delete Action Method
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var productType = _dbContext.Categories.Find(Id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //Post Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id,Category productTypes)
        {
            if (Id==null && Id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _dbContext.Categories.Find(Id);

            _dbContext.Remove(productType);
            await _dbContext.SaveChangesAsync();
            TempData["save"] = "Product Type Deleted";

            return RedirectToAction("Index");
        }
    }
}
