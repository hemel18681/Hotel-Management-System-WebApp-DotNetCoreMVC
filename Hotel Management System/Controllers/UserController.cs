using Hotel_Management_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var data = db.new_room.ToList().Where(x => x.room_status == true);
            return View(data);
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
        public IActionResult BookARoom(int customerId, int roomId)
        {
            var roomChoose = db.new_room.Where(x => x.id == roomId).FirstOrDefault();
            roomChoose.room_choose = true;
            db.Entry(roomChoose).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CheckInRooms", new {@customerId = customerId });
        }

        [HttpGet]
        public IActionResult RemoveARoom(int customerId, int roomId)
        {
            var roomChoose = db.new_room.Where(x => x.id == roomId).FirstOrDefault();
            roomChoose.room_choose = false;
            db.Entry(roomChoose).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CheckInRooms", new { @customerId = customerId });
        }

        [HttpGet]
        public IActionResult CheckInRooms(int customerId)
        {
            CheckInModel userData = new CheckInModel();
            var userList = (CustomerDetails)db.customer_info.Where(x => x.customer_id == customerId).First();
            userData.CheckInUser = userList;
            var availRoom = db.new_room.Where(x => x.room_status == true && x.room_choose==false).ToList();
            var roomList = db.new_room.Where(x => x.room_choose == true).ToList();
            userData.RoomList = roomList;
            userData.AvailRoom = availRoom;
            userData.CheckInUserData = new CheckInTmpModel() ;
            userData.CheckInUserData.CustomerId = customerId;
            return View(userData);
        }

        [HttpPost]
        public IActionResult BookRooms(CheckInModel customerTmpData)
        {
            var roomList = db.new_room.Where(x => x.room_choose == true).ToList();
            var date = DateTime.Now.ToString("ddd/dd/MM/yyyy/mm/HH");
            var invoice = date[0] + "" + date[1] + "" + date[4] + "" + date[5] + "" + date[7] + "" + date[8] + "" + date[10] + "" + date[11] + "" + date[12] + "" + date[13] + "" + date[15] + "" + date[16] + "" + date[18] + "" + date[19];
            var customerData = db.customer_info.Where(x => x.customer_id == customerTmpData.CheckInUserData.CustomerId).FirstOrDefault();
            customerData.checked_in = true;
            customerData.invoice_no = invoice;
            db.Entry(customerData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var roomPrice = "";
            for(int i = 0; i < roomList.Count; i++)
            {
                if (roomPrice.Length == 0) roomPrice += roomList[i].room_price.ToString();
                else roomPrice+="\n"+ roomList[i].room_price.ToString();
            }
            for (int i = 0; i < roomList.Count; i++)
            {
                ReportModel report = new ReportModel();
                roomList[i].room_booked_by = customerTmpData.CheckInUserData.CustomerId;
                roomList[i].room_status = false;
                roomList[i].room_choose = false;
                roomList[i].room_booked_date = DateTime.Now.ToString("dd/MM/yyyy");
                roomList[i].room_booked_hour = Convert.ToInt32(DateTime.Now.ToString("HH"));
                roomList[i].room_booked_minute = Convert.ToInt32(DateTime.Now.ToString("mm"));
                db.Entry(roomList[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                report.invoice_no = invoice;
                report.check_in_time = DateTime.Now;
                report.customer_name = customerData.customer_name;
                report.customer_phone = customerData.customer_phone;
                report.customer_from = customerData.customer_from;
                report.room_no = roomList[i].room_no;
                if(i==0) report.room_price = roomPrice;
                report.checked_out = false;
                db.report_data.Add(report);
            }
            db.SaveChanges();
            return RedirectToAction("UserWork");
        }


        [HttpGet]
        public IActionResult CheckedInShow()
        {
            var checkedInList = db.customer_info.Where(x => x.checked_in == true);
            return View("CheckedInShow", checkedInList);
        }


        [HttpGet]
        public IActionResult CreateExpense()
        {
            var expense = new ExpenseView();
            expense.view_expense = db.expense_data.Where(x => x.entry_date >= DateTime.Today).ToList();
            return View("CreateExpense", expense);
        }
        [HttpPost]
        public IActionResult CreateExpense(ExpenseModel expense)
        {
            expense.entry_date = DateTime.Now;
            db.expense_data.Add(expense);
            db.SaveChanges();
            return RedirectToAction("UserWork");
        }

        [HttpPost]
        public IActionResult ViewExpense(ExpenseGet expenseGet)
        {
            var expense = new ExpenseView();
            var expenseView = db.expense_data.Where(x => x.entry_date >= expenseGet.fromDate && x.entry_date <= expenseGet.toDate).ToList();
            expense.view_expense = expenseView;
            return View("ViewExpense", expense);
        }

        [HttpGet]
        public IActionResult CheckoutForm(int customerId)
        {
            CheckOutFormModel data = new CheckOutFormModel();
            var userData = db.customer_info.Where(x => x.customer_id == customerId).FirstOrDefault();
            data.userData = userData;
            data.cc = data.userData.customer_id;
            List<NewRoom> roomData = db.new_room.Where(x=>x.room_booked_by==customerId).ToList();
            data.roomData = roomData;
            data.entryDate = roomData[0].room_booked_date;
            var bookingHour = roomData[0].room_booked_hour.ToString();
            var bookingMinute = roomData[0].room_booked_minute.ToString();
            if (bookingHour.Length == 1) { bookingHour = "0" + bookingHour; }
            if (bookingMinute.Length == 1) { bookingMinute = "0" + bookingMinute; }
            data.entryTime = bookingHour + ":" + bookingMinute;
            data.checkoutDate = DateTime.Now.ToString("dd/MM/yyyy");
            data.checkoutTime = DateTime.Now.ToString("HH:mm");
            data.advanceAmount = userData.advance_amount;
            data.totalStayDays = Convert.ToInt32((DateTime.Now - DateTime.ParseExact(data.entryDate, "dd/MM/yyyy", null)).TotalDays);
            for (int i = 0; i < roomData.Count; i++)
            {
                data.totalRent += roomData[i].room_price;
            }
            data.totalCost = data.totalRent * data.totalStayDays;
            data.remainingAmount = data.totalCost - data.advanceAmount;
            return View(data);
        }
        [HttpPost]
        public IActionResult Checkout(CheckOutFormModel checkout)
        {
            var userData = db.customer_info.Where(x => x.customer_id == checkout.cc).FirstOrDefault();
            List<NewRoom> roomData = db.new_room.Where(x => x.room_booked_by == checkout.cc).ToList();
            var reportData = db.report_data.Where(x => x.invoice_no == userData.invoice_no).ToList();
            for(int i = 0; i < reportData.Count; i++)
            {
                reportData[i].check_out_time = DateTime.Now;
                reportData[i].room_total = (Convert.ToDouble(reportData[i].room_price) * checkout.totalStayDays).ToString();
            }
            int last = reportData.Count - 1;
            reportData[last].sub_total = checkout.totalCost;
            reportData[last].discount = checkout.discountAmount;
            reportData[last].advance_amount = userData.advance_amount;
            reportData[last].grand_total = checkout.remainingAmount;
            reportData[last].checked_out = true;
            reportData[last].stayed = checkout.totalStayDays;
            for(int i = 0; i < roomData.Count; i++)
            {
                roomData[i].room_booked_by = 0;
                roomData[i].room_status = true;
                db.Entry(roomData[i]).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            userData.checked_in = false;
            userData.invoice_no = "";
            userData.advance_amount = 0;
            db.Entry(userData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            ViewBag.message = "Checked Out Complete";
            return RedirectToAction("UserWork");
        }
    }
}
