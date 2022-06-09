using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class CheckOutFormModel
    {
        public CustomerDetails userData { get; set; }
        public List<NewRoom> roomData { get; set; }
        public string checkoutDate { get; set; }
        public string checkoutTime { get; set; }
        public string entryDate { get; set; }
        public decimal totalRent { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal remainingAmount { get; set; }
        public int totalStayDays { get; set; }
        public decimal discountAmount { get; set; }
    }
}
