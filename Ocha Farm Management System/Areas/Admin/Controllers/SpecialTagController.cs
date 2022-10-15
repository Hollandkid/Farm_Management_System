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
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecialTagController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //This is the Home Page
        public IActionResult Index()
        {
            var data = _dbContext.Brands.ToList();
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
        public async Task<IActionResult> Create(Brand tagModel)
        {
            if (ModelState.IsValid)
            {

                //Add to Db
                _dbContext.Brands.Add(tagModel);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Special Tag has been Saved";

                return RedirectToAction("Index");
            }

            return View(tagModel);
        }



        //Get Edit Action Method
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var specialTag = _dbContext.Brands.Find(Id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand specialTagModel)
        {
            if (ModelState.IsValid)
            {

                //Uodate Db
                _dbContext.Brands.Update(specialTagModel);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Special Tag Updated";

                return RedirectToAction("Index");
            }

            return View(specialTagModel);
        }



        //Get Details Action Method
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var specialTag = _dbContext.Brands.Find(Id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }


        //Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Brand specialTagModel)
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

            var specialTags = _dbContext.Brands.Find(Id);
            if (specialTags == null)
            {
                return NotFound();
            }

            return View(specialTags);
        }

        //Post Delete Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, Brand specialTagModel)
        {
            if (Id == null && Id != specialTagModel.Id)
            {
                return NotFound();
            }
            var specialTag = _dbContext.Brands.Find(Id);

            _dbContext.Remove(specialTag);
            await _dbContext.SaveChangesAsync();
            TempData["save"] = "Special Tag Deleted";
            return RedirectToAction("Index");
        }
    }
}
