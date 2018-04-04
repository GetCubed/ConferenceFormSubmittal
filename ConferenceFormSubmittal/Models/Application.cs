using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Application : Auditable
    {
        public Application()
        {
            Mileages = new HashSet<Mileage>();
            Expenses = new HashSet<Expense>();
        }

        public int ID { get; set; }

        [StringLength(500, ErrorMessage = "Rationale may include a maximum of 500 characters.")]
        public string Rationale { get; set; }

        [Display(Name = "Replacement Staff Required")]
        public bool ReplStaffReq { get; set; }

        public string BudgetCode { get; set; }

        // The date on which they will travel to the conference
        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        // The date on which they will return from the conference
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }

        // The first day they will attend the conference
        [Display(Name = "Start of Attendance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendStartDate { get; set; }

        // The last day they will attend the conference
        [Display(Name = "End of Attendance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendEndDate { get; set; }

        // The actual submission date (Applications can be saved as drafts)
        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateSubmitted { get; set; }

        // Administrator feedback
        [StringLength(500, ErrorMessage = "Feedback may include a maximum of 500 characters.")]
        public string Feedback { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeID { get; set; } = 1; // Using default value until user roles are implemented

        [Display(Name = "Conference")]
        public int ConferenceID { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; } = 1; // Default status is "Submitted"

        [Display(Name = "Payment Type")]
        public int PaymentTypeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual Status Status { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual ICollection<Mileage> Mileages { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}