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

        public string BudgetCode { get; set; }

        // The date on which they will travel to the conference
        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DepartureDate { get; set; }

        // The date on which they will return from the conference
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }

        // The first day they will attend the conference
        [Display(Name = "Attendance Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AttendStartDate { get; set; }

        // The last day they will attend the conference
        [Display(Name = "Attendance End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AttendEndDate { get; set; }

        // The actual submission date (Applications can be saved as drafts)
        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateSubmitted { get; set; }

        // Administrator feedback
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