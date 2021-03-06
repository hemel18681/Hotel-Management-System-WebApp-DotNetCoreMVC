using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class CheckInModel
    {
        [Key]
        public CustomerDetails CheckInUser { get; set; }
        public List<NewRoom> RoomList { get; set; }
        public List<NewRoom> AvailRoom { get; set; }
        public CheckInTmpModel CheckInUserData { get; set; }

    }
    public class CheckInTmpModel
    {
        public int CustomerId { get; set; }
        public double AdvanceAmount { get; set; }
    }
}
