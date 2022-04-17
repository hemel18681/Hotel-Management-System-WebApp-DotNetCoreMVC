using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class ReportModel
    {
        [Key]
        public int id { get; set; }
        public string invoice_no { get; set; }
        public DateTime check_in_time { get; set; }
        public DateTime check_out_time { get; set; }
        public string customer_name { get; set; }
        public long customer_phone { get; set; }
        public string customer_from { get; set; }
        public string room_no { get; set; }
        public string room_price { get; set; }
        public int stayed { get; set; }
        public string room_total { get; set; }
        public decimal sub_total { get; set; }
        public decimal discount { get; set; }
        public decimal grand_total { get; set; }
        public decimal advance_amount { get; set; }


    }
}
