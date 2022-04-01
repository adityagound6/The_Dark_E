using FoodRest.Models;
using FoodRest.Models.SqlRepositry;
using Microsoft.AspNetCore.Mvc;
using FoodRest.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepositry _Irepo;
        private readonly AppDbContext db;
        public AdminController(AppDbContext db,IRepositry _Irepo)
        {
            this.db = db;
            this._Irepo = _Irepo;
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


    }
}
