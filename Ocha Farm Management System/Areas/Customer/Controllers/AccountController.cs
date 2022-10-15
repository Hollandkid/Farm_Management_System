using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ocha_Farm_Management_System.Data;
using Ocha_Farm_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocha_Farm_Management_System.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext,  SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterModel model, string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("/Customer/Home/Index");
            if (model != null)
            {
                if (model.Email != null && model.Password != null)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if(user!= null)
                    {
                        var isUser = await _userManager.IsInRoleAsync(user, "User");
                        if (!isUser)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt, You are not a Customer \n Only Customers are allowed to Sign In.");
                            return View(model);
                        }

                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check Login credentials");
                            return View(model);
                        }
                        TempData["save"] = "Welcome User";
                        return LocalRedirect(returnUrl);
                    }
                   
                }
            }
            
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {

                //Add to Db
                var newUser = new ApplicationUser()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    UserName = user.Email,
                    Role = "User",
                    Password=user.Password
                };
                

                var chkuser = await _userManager.CreateAsync(newUser, user.Password);

                if (chkuser.Succeeded)
                {
                    var result = await _userManager.AddToRoleAsync(newUser, "User");
                    if (result.Succeeded)
                    {
                        //Email Authentication is Required

                        TempData["save"] = "You have successfully Register as a User";
                        await _dbContext.SaveChangesAsync();

                        var SignedIn = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
                        if (SignedIn.Succeeded)
                        {
                            TempData["save"] = "Welcome User";
                            //return LocalRedirect(returnUrl);

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check Login credentials");
                            return View();
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in chkuser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View("Login",user);
            //return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
