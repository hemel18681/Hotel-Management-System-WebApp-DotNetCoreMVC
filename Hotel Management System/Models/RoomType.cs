using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class RoomType
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string room_type { get; set; }
        public double room_price { get; set; }
    }
}
