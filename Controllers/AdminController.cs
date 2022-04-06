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
            var mod = new ListOfUsersViewModel();
            mod.user = new Users();
            mod.user.Name = user.Name;
            mod.user.UserName = user.UserName;
            mod.user.Address = user.Address;
            mod.user.Email = user.Email;
            mod.user.PostCode = user.PostCode;
            mod.user.Role = user.Role;
            mod.user.UserId = user.UserId;
            //mod.user.Name = user.Name;
           
            var model = new UserDetailsViewModel()
            {
                Name = user.Name,
                UserName = user.UserName,
                Image = user.Image,
                Address = user.Address,
                Email = user.Email,
                PostCode = user.PostCode,
                Role = user.Role,
                UserId = user.UserId
            };
            return View(mod);
        }

        //User CRUD
        #region

        public IActionResult ListOfUsers(int userId)
        {
            //var user = db.Users.Find(userId);
            List<Users> mod = _Irepo.GetAllEmployee().ToList();
            ListOfUsersViewModel model = new ListOfUsersViewModel();
            var user = db.Users.Find(userId);
            //var model = new ListOfUsersViewModel();
            model.user = new Users();
            model.user.Name = user.Name;
            model.user.UserName = user.UserName;
            model.user.Address = user.Address;
            model.user.Email = user.Email;
            model.user.PostCode = user.PostCode;
            model.user.Role = user.Role;
            model.user.UserId = user.UserId;
            if (model.ListOfUser == null)
            {
                model.ListOfUser = new List<Users>();
            }
            foreach(var use in mod)
            {
                model.ListOfUser.Add(use);
            }
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


        //Category CRUD

        #region

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult CreateCategory(CategoryListViewModel model)
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
        [HttpGet]
        public IActionResult EditCategory(int catId)
        {

            var cat = db.Categories.Find(catId);
            var model = new EditCategory()
            {
                Name = cat.Name,
                NewPhoto = cat.ImageUrl,
                CategoryId = cat.CategoryId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditCategory(int CatId,EditCategory model)
        {
            if (ModelState.IsValid)
            {
                var category = db.Categories.Find(CatId);
                if(category == null)
                {
                    return View(model);
                }
                category.Name = model.Name;
                category.ImageUrl = model.NewPhoto;
                db.SaveChanges();
                return RedirectToAction("Dashboard");
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
        public IActionResult CategoryList()
        {
            var model = db.Categories;
            return View(model);
        }
        public IActionResult DetailCategory(int CatId)
        {
            var model = db.Categories.Find(CatId);
            return View(model);
        }


        #endregion



        //Product CRUD
        #region
        public IActionResult ProductList()
        {
            var model = db.Products;
            return View(model);
        }
        [HttpGet]
        public IActionResult EditProduct(int prodId)
        {
            var model = db.Products.Find(prodId);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(int prodId,EditProductViewModel model)
        {
            var prod = db.Products.Find(prodId);
            Product product = new Product
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Qantity = model.Qantity,
                Price = model.Price,
                ImageUrl = model.NewPhoto
            };
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        public IActionResult DeleteProduct(int prodId)
        {
            var product = db.Products.Find(prodId);
            if(product == null)
            {
                return Json("Notm found");

            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Price = model.Price,
                    CreateDate = DateTime.Now,
                    Qantity = model.Qantity,
                    ImageUrl = model.ImageUrl,
              
                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        #endregion


        public IActionResult ListOrder()
        {
            var model = db.Orders;
            return View(model);
        }

        [Obsolete]
        public string ProcessUploadFile(CategoryListViewModel model)
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
