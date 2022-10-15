using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ocha_Farm_Management_System.Data;
using Ocha_Farm_Management_System.Models;
using Ocha_Farm_Management_System.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Ocha_Farm_Management_System.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int? page)
        {

            var catResult = new List<Category>();
            //I used the X.PagedList for the MVC which was download from the Nuget Manager... Also use the Method
            var result = _dbContext.Products.Include(c => c.Categories).Include(f => f.Brands).ToList().ToPagedList(page ?? 1, 28);
            catResult = _dbContext.Categories.ToList();

            //This is using DOT
            var productVM = new ProductViewModel
            {
                Products = result,
                ProductTypes = catResult
            };
            if (result == null)
            {
                return View();
            }
            if (catResult == null)
            {
                catResult = new List<Category>();
            }

            return View(productVM);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Cart()
        {
            List<Product> productModels = HttpContext.Session.Get<List<Product>>("productModel");

            if (productModels == null)
            {
                productModels = new List<Product>();
            }

            //var AllProduct = new AllProductViewModel
            //{
            //    Products = productModels
            //};

            return View(productModels);
        }

        public IActionResult Checkout(string Id)
        {
            CheckoutDetailsViewModel VMMOdel = new CheckoutDetailsViewModel();
            List<Product> productModels = HttpContext.Session.Get<List<Product>>("productModel");

            if (productModels == null)
            {
                productModels = new List<Product>();
            }
            var userInfo = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == Id);
            if (userInfo == null)
            {
                userInfo = new ApplicationUser();
            }

            VMMOdel.Products = productModels;
            VMMOdel.User = userInfo;

            return View(VMMOdel);

        }


        public IActionResult Farmer()
        {
            CheckoutDetailsViewModel VMMOdel = new CheckoutDetailsViewModel();
            List<Product> productModels = _dbContext.Products.OrderBy(d => d.DateCreated).ToList();

            if (productModels == null)
            {
                productModels = new List<Product>();
            }
            var userInfo = _dbContext.ApplicationUsers.Where(c => c.Role == "Admin").ToList();
            if (userInfo == null)
            {
                userInfo = new List<ApplicationUser>();
            }

            VMMOdel.Products = productModels;
            VMMOdel.Users = userInfo;

            return View(VMMOdel);

        }

        public IActionResult Farm(string Id)
        {
            FarmViewModel VMMOdel = new FarmViewModel();
            List<Product> productModels = _dbContext.Products.OrderBy(d => d.DateCreated).ToList();

            if (productModels == null)
            {
                productModels = new List<Product>();
            }
            var farms = _dbContext.Farm.ToList();
            if (farms == null)
            {
                farms = new List<FarmDetails>();
            }

            VMMOdel.Products = productModels;
            VMMOdel.Farms = farms;

            return View(VMMOdel);

        }


        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }


        public IActionResult Remove(int? Id)
        {
            //get the model
            List<Product> productModels = HttpContext.Session.Get<List<Product>>("productModel");

            if (productModels != null)
            {
                var product = productModels.FirstOrDefault(g => g.Id == Id);

                if (product != null)
                {
                    productModels.Remove(product);
                    HttpContext.Session.Set("productModel", productModels);
                }
            }
            return RedirectToAction("Cart");
        }

        //Post Remove ActionMethod
        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemovefromCart(int? Id)
        {
            //get the model
            List<Product> productModels = HttpContext.Session.Get<List<Product>>("productModel");

            if (productModels != null)
            {
                var product = productModels.FirstOrDefault(g => g.Id == Id);

                if (product != null)
                {
                    productModels.Remove(product);
                    HttpContext.Session.Set("productModel", productModels);
                }
            }
            return RedirectToAction("Cart");
        }

        public IActionResult ProductDetails(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                //var result = _dbContext.Products.Find(Id);
                var product = _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).FirstOrDefault(f => f.Id == Id);
                var products = _dbContext.Products.Include(c => c.Categories).Include(f => f.Brands).ToList();
                var ProductsVM = new AllProductViewModel
                {
                    Product = product,
                    Products = products
                };
                if (product == null) return NotFound();

                return View(ProductsVM);
            }

        }

        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetailsZ(String use)
        {
            //get the model
            List<Product> productModels = new List<Product>();

            if (use == null)
            {
                return View();
            }
            else
            {
                //var result = _dbContext.Products.Find(Id);
                var product = _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).FirstOrDefault(f => f.Id == Int32.Parse(use));
                var products = await _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).ToListAsync();
                if (product == null)
                {
                    return NotFound();
                }

                productModels = HttpContext.Session.Get<List<Product>>("productModel");    //This is to set the Session to the model
                if (productModels == null)
                {
                    productModels = new List<Product>();

                }
                productModels.Add(product);
                HttpContext.Session.Set("productModel", productModels);     //This isto set the Session to the model

                var Allproduct = new AllProductViewModel
                {
                    Product = product,
                    Products = products
                };

                return View(Allproduct);
            }

        }

        [HttpPost]
        public async Task<IActionResult> ProductAdd(String use)
        {
            //get the model
            List<Product> productModels = new List<Product>();

            if (use == null)
            {
                return View("Index");
            }
            else
            {
                //var result = _dbContext.Products.Find(Id);
                var product = _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).FirstOrDefault(f => f.Id == Int32.Parse(use));
                var products = await _dbContext.Products.Include(c => c.Categories).Include(g => g.Brands).ToListAsync();
                if (product == null)
                {
                    return NotFound();
                }

                productModels = HttpContext.Session.Get<List<Product>>("productModel");    //This is to set the Session to the model
                if (productModels == null)
                {
                    productModels = new List<Product>();

                }
                productModels.Add(product);
                HttpContext.Session.Set("productModel", productModels);     //This isto set the Session to the model

                var Allproduct = new AllProductViewModel
                {
                    Product = product,
                    Products = products
                };

                int? page = 1;
                var catResult = new List<Category>();
                //I used the X.PagedList for the MVC which was download from the Nuget Manager... Also use the Method
                var result = _dbContext.Products.Include(c => c.Categories).Include(f => f.Brands).ToList().ToPagedList(page ?? 1, 28);
                catResult = _dbContext.Categories.ToList();

                //This is using DOT
                var productVM = new ProductViewModel
                {
                    Products = result,
                    ProductTypes = catResult
                };
                if (result == null)
                {
                    return View();
                }
                if (catResult == null)
                {
                    catResult = new List<Category>();
                }
                return View("index", productVM);
            }

        }
    }
}
