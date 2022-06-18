using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_System.Models
{
    public class ExpenseModel
    {
        [Key]
        public int id { get; set; }
        public DateTime entry_date { get; set; }
        public string expense_details { get; set; }
        public double expense_cost { get; set; }
    }

    public class ExpenseView
    {
        public string expense_details { get; set; }
        public double expense_cost { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public ExpenseModel new_expense { get; set; }
        public List<ExpenseModel> view_expense { get; set; }
    }

    public class ExpenseGet
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }

}
