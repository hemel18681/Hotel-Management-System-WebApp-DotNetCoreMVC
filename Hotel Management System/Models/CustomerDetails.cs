using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class CustomerDetails
    {
        [Key]
        [Required]
        public int customer_id{ get; set; }
        public int customer_phone { get; set; }
        public string customer_name { get; set; }
        public long customer_nid { get; set; }
        public string customer_from { get; set; }
        public string image_name { get; set; }
        public string image_path { get; set; }
        public bool checked_in { get; set; }
        public decimal advance_amount { get; set; }
    }
}
