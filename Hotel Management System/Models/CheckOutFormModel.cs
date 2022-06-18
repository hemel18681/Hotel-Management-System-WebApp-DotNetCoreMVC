using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class CheckOutFormModel
    {
        [Key]
        public int cc { get; set; }
        public CustomerDetails userData { get; set; }
        public List<NewRoom> roomData { get; set; }
        public string checkoutDate { get; set; }
        public string checkoutTime { get; set; }
        public string entryDate { get; set; }
        public string entryTime { get; set; }
        public double totalRent { get; set; }
        public double advanceAmount { get; set; }
        public double remainingAmount { get; set; }
        public int totalStayDays { get; set; }
        public double discountAmount { get; set; }
        public double totalCost { get; set; }
    }
}
