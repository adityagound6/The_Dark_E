using Microsoft.AspNetCore.Mvc;
using FoodRest.ViewModel.Accounts;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FoodRest.Models;
using FoodRest.Models.SqlRepositry;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace FoodRest.Controllers
{
    public class AccountController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly AppDbContext db;
        private readonly IRepositry _Irepo;

        [Obsolete]
        public AccountController(IHostingEnvironment hostingEnvironment, IRepositry _Irepo,
                                  AppDbContext db)
        {
            this._Irepo = _Irepo;
            this.hostingEnvironment = hostingEnvironment;
            this.db = db;
        }
        [HttpGet]
        public IActionResult RegisterUsers()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult RegisterUsers(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = ProcessUploadFile(model);
                Users newUser = new Users
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                    CreateDate = DateTime.Now,
                    Name = model.Name,
                    Image = fileName,
                    Password = model.Password,
                    PostCode = model.PostCode,
                    Role = "User"
                };
                _Irepo.AddUsers(newUser);
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("Role",newUser.Role);
                ViewBag.Session = HttpContext.Session.GetString("Email");
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValid = _Irepo.IsUser(model);
                if (isValid)
                {
                    var result = _Irepo.GetUserById(model);
                    HttpContext.Session.SetString("Email", model.Email);
                    ViewBag.Session = HttpContext.Session.GetString("Email");
                    HttpContext.Session.SetString("Admin", result.Role);
                    ViewBag.Role = HttpContext.Session.GetString("Admin");
                    if(result.Role == "Admin")
                    {
                        return RedirectToAction("Dashboard","Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Either email or password wrong");
            }
            return View(model);
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        //process of file uploading
        [Obsolete]
        private string ProcessUploadFile(RegisterViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string photoUpload = Path.Combine(hostingEnvironment.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(photoUpload, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
