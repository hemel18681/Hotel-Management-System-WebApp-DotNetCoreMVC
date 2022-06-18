using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Data;
using Hotel_Management_System.Models;


namespace Hotel_Management_System.Models
{
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }
            public DbSet<UserDetails> user_info { get; set; }
            public DbSet<CustomerDetails> customer_info { get; set; }
            public DbSet<RoomType> room_type { get; set; }
            public DbSet<NewRoom> new_room { get; set; }
            public DbSet<ExpenseModel> expense_data { get; set; }
            public DbSet<ReportModel> report_data { get; set; }
            public DbSet<Hotel_Management_System.Models.ViewReportGetModel> ViewReportGetModel { get; set; }
        }
}
