using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class NewRoom
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string room_type { get; set; }
        public double room_price { get; set; }
        public int room_floor { get; set; }
        public bool room_status { get; set; }
        public int room_no { get; set; }
        public int room_booked_by { get; set; }
        public bool room_choose { get; set; }
        public string room_booked_date { get; set; }
        public int room_booked_hour { get; set; }
        public int room_booked_minute { get; set; }

    }
}
