using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Mileage
    {
        public int ID { get; set; }

        [Display(Name = "Date of Travel")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TravelDate { get; set; }

        [Display(Name = "Starting Address")]
        [Required(ErrorMessage = "Starting Address is required.")]
        [StringLength(100)]
        public string StartAddress { get; set; }

        [Display(Name = "Destination Address")]
        [Required(ErrorMessage = "Destination Address is required.")]
        [StringLength(100)]
        public string EndAddress { get; set; }

        [Display(Name = "Round Trip")]
        public bool RoundTrip { get; set; }

        // to be calculated
        [Display(Name = "Distance")]
        public decimal Kilometres { get; set; }

        [StringLength(500, ErrorMessage = "Feedback may include no more than 500 characters.")]
        public string Feedback { get; set; }

        public int StatusID { get; set; }

        public int EmployeeID { get; set; }

        public int? ApplicationID { get; set; }

        public virtual Status Status { get; set; }

        public virtual Employee Employee { get; set; }

        [Display(Name = "Conference")]
        public virtual Application Application { get; set; }
    }
}