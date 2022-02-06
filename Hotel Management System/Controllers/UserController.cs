using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Controllers
{
    public class UserController : Controller
    {
        public DataContext db;
        private readonly IWebHostEnvironment _environment;
        public UserController(DataContext _db, IWebHostEnvironment environment)
        {
            db = _db;
            _environment = environment;
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
        [HttpPost]
        public IActionResult Capture(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if(files!=null)
                {
                    foreach(var file in files)
                    {
                        if(file.Length>0)
                        {
                            var filename = file.FileName;
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var fileExtension = Path.GetExtension(filename);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filePath = Path.Combine(_environment.WebRootPath,"CameraPhotos") + $@"\{newFileName}";
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                StoreInFolder(file, filePath);
                            }
                            var imageBytes = System.IO.File.ReadAllBytes(filePath);
                            if(imageBytes !=null)
                            {
                                StoreInDatabase(imageBytes);
                            }
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
        private void StoreInDatabase(byte[] imageBytes)
        {

        }
    }
}
