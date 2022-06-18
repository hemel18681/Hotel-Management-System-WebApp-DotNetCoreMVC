using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult EditRoomTypeList()
        {
            return View(db.room_type.ToList());
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

        [HttpGet]
        public IActionResult AddRoomType()
        {
            return View("AddRoomType", new RoomType());
        }
        [HttpPost]
        public IActionResult AddRoomType(RoomType user)
        {
            string msg;
            var account = db.room_type.Where(a => a.room_type == user.room_type).FirstOrDefault();
            if (account == null)
            {
                db.room_type.Add(user);
                db.SaveChanges();
                return RedirectToAction("AdminWork");
            }
            else
            {
                msg = "Already exist as a room type.";
            }
            ViewBag.message = msg;
            return View("AddRoomType");

        }


        public IActionResult EditNewRoomList()
        {
            return View(db.new_room.ToList());
        }


        [HttpGet]
        public IActionResult AddNewRoom()
        {
            var roomType = db.room_type.ToList();
            
            List<IdDescription> myList = new List<IdDescription>();
            for(int i=0;i<roomType.Count;i++)
            {
                IdDescription model = new IdDescription();
                model.Id = roomType[i].id;
                model.Description = roomType[i].room_type;
                myList.Add(model);
            }
            ViewBag.roomTypeList = myList;
            return View("AddNewRoom", new NewRoom());
        }
        [HttpPost]
        public IActionResult AddNewRoom(NewRoom user)
        {
            if(user.room_type=="0")
            {
                ViewBag.Message = "Please Select a Room Type";
                return RedirectToAction("AddNewRoom");
            }
            var roomCount = db.new_room.Where(x => x.room_floor == user.room_floor).Count();
            user.room_no = (Convert.ToInt32(user.room_floor) * 100) + (roomCount + 1);
            user.room_status = true;
            db.new_room.Add(user);
            db.SaveChanges();
            return RedirectToAction("AdminWork");
        }

        [Route("Admin/GetRoomPrice")]
        [HttpGet]
        public IActionResult GetRoomPrice(string name)
        {
            var data = db.room_type.Where(x => x.room_type == name).FirstOrDefault();
            return Ok(data.room_price);
        }
        [HttpGet]
        public IActionResult DeleteRoom(int id)
        {
            NewRoom a = db.new_room.Single(x => x.id == id);
            db.new_room.Remove(a);
            db.new_room.Remove(a);
            db.SaveChanges();
            return RedirectToAction("EditNewRoomList");
        }
        [HttpGet]
        public IActionResult ViewReportSelect()
        {
            return View("ViewReportSelect", new ViewReportGetModel());
        }

        [HttpPost]
        public IActionResult ViewReportSelect(ViewReportGetModel model)
        {
            var reportData = db.report_data.Where(x => x.check_out_time >= model.fromDate && x.check_out_time <= model.toDate && x.checked_out == true).ToList();
            return View("ReportView", reportData);
        }
    }
}
