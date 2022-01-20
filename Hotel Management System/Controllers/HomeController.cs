using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public DataContext db;
        public HomeController(DataContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            int cheak = 1;
            var active = db.user_info.Where(a => a.status == cheak).FirstOrDefault();
            if (active == null && HttpContext.Session.GetInt32("adminId") == null)
            {
                return View();
            }
            else if (HttpContext.Session.GetInt32("adminId") == null)
            {
                return RedirectToAction("Logout");
            }
            else
            {
                return RedirectToAction("AdminWork");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserDetails ud)
        {
            string msg;
            var account = db.user_info.Where(a => a.user_phone == ud.user_phone).FirstOrDefault();
            if (account != null)
            {
                if (account.user_password == ud.user_password)
                {
                    msg = "";
                    HttpContext.Session.SetInt32("adminId", account.user_id);
                    HttpContext.Session.SetString("adminUserName", account.user_name);
                    HttpContext.Session.SetInt32("userType", account.user_type);

                    account.status = 1;
                    db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    if(account.user_type==1)
                        return RedirectToAction("AdminWork", "Admin", new { area = "" });
                    else
                        return RedirectToAction("UserWork", "User", new { area = "" });
                }
                else
                {
                    msg = "Id or password is wrong";
                }
            }
            else
            {
                msg = "Id or password is wrong";
            }
            ViewBag.message = msg;
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            int cheak =1;
            var active = db.user_info.Where(a => a.status == cheak).FirstOrDefault();
            if (active != null)
            {
                active.status = 0;
                db.Entry(active).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
