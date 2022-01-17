using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class UserDetails
    {
        [Key]
        [Required]
        public int user_id { get; set; }
        public int user_phone { get; set; }
        public string user_name { get; set; }
        public int user_type { get; set; }
        [Required]
        public string user_password { get; set; }
        public int status { get; set; }
    }
}
