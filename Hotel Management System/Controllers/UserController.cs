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
        public CheckInModel currentData;
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
        public IActionResult AddCustomer(CustomerDetails user)
        {
            string msg;
            var account = db.customer_info.Where(a => a.customer_phone == user.customer_phone).FirstOrDefault();
            if (account == null)
            {
                db.customer_info.Add(user);
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
                                //StoreInDatabase(imageBytes);
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
        //private void StoreInDatabase(byte[] imageBytes)
        //{

        //}
        public IActionResult EditCustomerList()
        {
            return View(db.customer_info.ToList());
        }

        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            string msg;
            CustomerDetails ad = db.customer_info.Find(id);
            if (ad == null)
            {
                msg = "This ID is not available";
                ViewBag.message = msg;
                return View("UserWork");
            }
            else
            {
                return View(ad);
            }

        }
        [HttpPost]
        public IActionResult EditCustomer(CustomerDetails ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                string msg = "Data updated successfully";
                ViewBag.message = msg;
                return View("UserWork");
            }
            else
            {
                return View(ad);
            }
        }

        [HttpGet]
        public IActionResult CustomerDetails(int customerId)
        {
            var data = db.customer_info.Where(x => x.customer_id == customerId);
            return View(data);
        }


        [HttpGet]
        public IActionResult AvailableRooms()
        {
            var data = db.new_room.ToList().Where(x => x.room_status == true);
            return View(data);
        }

        [HttpGet]
        public IActionResult CheckIn()
        {
            return View(db.customer_info.ToList());
        }
        [HttpGet]
        public IActionResult CheckInPage(int customerId)
        {
            var chooseData = db.new_room.Where(x => x.room_choose == true).ToList();
            for(int i = 0; i < chooseData.Count; i++)
            {
                chooseData[i].room_choose = false;
                db.Entry(chooseData[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            CheckInModel userData = new CheckInModel();
            var userList = (CustomerDetails) db.customer_info.Where(x=>x.customer_id==customerId).First();
            userData.CheckInUser = userList;
            var roomList =  db.new_room.Where(x => x.room_status == true).ToList();
            userData.AvailRoom = roomList;
            userData.RoomList = new List<NewRoom>();
            currentData = userData;
            return View(userData);
        }

        [HttpGet]
        public IActionResult CheckInRooms(int customerId, int roomId)
        {
            CheckInModel userData = new CheckInModel();
            var userList = (CustomerDetails)db.customer_info.Where(x => x.customer_id == customerId).First();
            userData.CheckInUser = userList;
            var roomChoose = db.new_room.Where(x => x.id == roomId).FirstOrDefault();
            roomChoose.room_choose = true;
            db.Entry(roomChoose).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            var availRoom = db.new_room.Where(x => x.room_status == true && x.room_choose==false).ToList();
            var roomList = db.new_room.Where(x => x.room_choose == true).ToList();
            userData.RoomList = roomList;
            userData.AvailRoom = availRoom;
            return View(userData);
        }

    }
}
