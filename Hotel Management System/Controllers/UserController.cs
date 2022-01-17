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
    }
}
