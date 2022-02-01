using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Controllers
{
    public class UserController : Controller
    {
        public DataContext db;
        public UserController(DataContext _db)
        {
            db = _db;
        }

        public IActionResult UserWork()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View("AddCustomer", new CustomerDetails());
        }
        [HttpPost]
        public IActionResult AddCustomer(UserDetails user)
        {
            string msg;
            var account = db.user_info.Where(a => a.user_phone == user.user_phone).FirstOrDefault();
            if (account == null)
            {
                db.user_info.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserWork");
            }
            else
            {
                msg = "Already exist as a Customer.";
            }
            ViewBag.message = msg;
            return View("AddCustomer");

        }
    }
}
