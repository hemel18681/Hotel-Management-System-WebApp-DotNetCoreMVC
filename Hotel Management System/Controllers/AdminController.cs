using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Controllers
{
    public class AdminController : Controller
    {
        public DataContext db;
        public AdminController(DataContext _db)
        {
            db = _db;
        }

        public IActionResult AdminWork()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View("AdminAddUser", new UserDetails());
        }
        [HttpPost]
        public IActionResult AddUser(UserDetails user)
        {
            string msg;
            var account = db.user_info.Where(a => a.user_phone == user.user_phone).FirstOrDefault();
            if (account == null)
            {
                db.user_info.Add(user);
                db.SaveChanges();
                return RedirectToAction("AdminWork");
            }
            else
            {
                msg = "Already exist as an User.";
            }
            ViewBag.message = msg;
            return View("AdminAddUser");

        }
    }
}
