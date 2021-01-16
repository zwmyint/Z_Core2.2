using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Models;
using EmployeeManager.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Controllers
{
    public class SecurityController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        //ctor
        public SecurityController(UserManager<AppIdentityUser> userManager,
              RoleManager<AppIdentityRole> roleManager,
              SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        //
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(m_cls_Register register)
        {
            if (ModelState.IsValid)
            {
                // for the first register (user role name will be Manager - Admin Role)
                if (!_roleManager.RoleExistsAsync("Manager").Result)
                {
                    var role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can Perform CRUD Operations";
                    var roleResult = _roleManager.CreateAsync(role).Result;
                }

                var user = new AppIdentityUser();
                user.UserName = register.UserName;
                user.Email = register.Email;
                user.FullName = register.FullName;
                user.BirthDate = register.BirthDate;

                var result = _userManager.CreateAsync(user, register.Password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Details");
                }
            }

            return View(register);
        }


        //[HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(m_cls_SignIn signIn)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, signIn.RememberMe, false).Result;

                if (result.Succeeded)
                    // successfuly signin
                    return RedirectToAction("List", "EmployeeManager");
                else
                    ModelState.AddModelError("", "Invalid User Details");
            }

            return View(signIn);
        }


        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
        }

        //
        public IActionResult AccessDenied()
        {
            return View();
        }



        //
    }
}