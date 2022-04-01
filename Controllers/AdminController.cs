using FoodRest.Models;
using FoodRest.Models.SqlRepositry;
using Microsoft.AspNetCore.Mvc;
using FoodRest.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using FoodRest.ViewModel.Accounts;
using System.IO;

namespace FoodRest.Controllers
{
    public class AdminController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IRepositry _Irepo;
        private readonly AppDbContext db;
        [Obsolete]
        public AdminController(AppDbContext db,IRepositry _Irepo, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this._Irepo = _Irepo;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Dashboard(int userId)
        {
            var user = db.Users.Find(userId);
            return View();
        }

        //User CRUD
        #region

        public IActionResult ListOfUsers()
        {
            var user = _Irepo.GetUser().FirstOrDefault();
            User model = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                UserId = user.UserId,
                CreateDate = user.CreateDate
            };
            return View(model);
        }
        public IActionResult UserDetails(int userId)
        {
            Users model = _Irepo.GetUsersById(userId);
            return View(model);
        }
        public IActionResult DeleteUser(int userId)
        {
            _Irepo.DeleteUsersById(userId);
            return RedirectToAction("ListOfUsers");
        }
        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            Users model = _Irepo.GetUsersById(userId);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditUser(int userId, Users model)
        {
            _Irepo.EditUser(userId, model);
            return RedirectToAction("ListOfUsers");
        }

        #endregion

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult CreateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filename = ProcessUploadFile(model);
                Categories cat = new Categories
                {
                    CreateDate = DateTime.Now,
                    Name = model.Name,
                    IsActive = true,
                    ImageUrl = filename
                };
            }
            return View(model);
        }
        public IActionResult EditCategory(int catId)
        {
            var cat = db.Categories.Find(catId);
            return View(cat);
        }
        [HttpPost]
        public IActionResult EditCategory(int CatId,CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(model);
        }

        public IActionResult DeleteCategory(int CatId)
        {
            var cat = db.Categories.Find(CatId);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [Obsolete]
        public string ProcessUploadFile(CategoryViewModel model)
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
