using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Application
    {
        public Application()
        {
            Mileages = new HashSet<Mileage>();
            Expenses = new HashSet<Expense>();
        }

        public int ID { get; set; }

        [StringLength(500, ErrorMessage = "Rationale may include a maximum of 500 characters.")]
        public string Rationale { get; set; }

        // Replacement Staff Required
        public bool ReplStaffReq { get; set; }

        // awaiting specifications for this field
        public string BudgetCode { get; set; }

        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateSubmitted { get; set; }

        [StringLength(500, ErrorMessage = "Feedback may include a maximum of 500 characters.")]
        public string Feedback { get; set; }

        public int EmployeeID { get; set; }

        public int ConferenceID { get; set; }

        public int StatusID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Mileage> Mileages { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}