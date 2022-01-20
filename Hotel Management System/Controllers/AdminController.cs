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
        public IActionResult EditUserList()
        {
            return View(db.user_info.ToList());
        }

        [HttpGet]
        public IActionResult Editprofile(int id)
        {
            string msg;
            UserDetails ad = db.user_info.Find(id);
            if (ad == null)
            {
                msg = "This ID is not available";
                ViewBag.message = msg;
                return View("AdminWork");
            }
            else
            {
                return View(ad);
            }

        }
        [HttpPost]
        public IActionResult EditProfile(UserDetails ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return View("AdminWork");
            }
            else
            {
                return View(ad);
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            UserDetails a = db.user_info.Single(x => x.user_id == id);
            db.user_info.Remove(a);
            db.SaveChanges();
            return View("AdminWork");
        }

    }
}
