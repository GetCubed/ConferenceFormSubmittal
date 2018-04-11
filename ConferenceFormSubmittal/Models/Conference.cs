using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Conference: IValidatableObject
    {
        public Conference()
        {
            Applications = new HashSet<Application>();
        }

        public int ID { get; set; }

        [Display(Name = "Conference")]
        [Required(ErrorMessage = "Conference Name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Conference Name must be between 5 and 100 characters in length.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100)]
        public string Location { get; set; }

        [Display(Name = "Registration Cost")]
        [Required(ErrorMessage = "Registration Cost is required.")]
        [DataType(DataType.Currency)]
        public decimal RegistrationCost { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult("End Date cannot be before Start Date.", new[] { "EndDate" });
            }
            if (StartDate < DateTime.Today)
            {
                yield return new ValidationResult("Start Date cannot be in the past.", new[] { "StartDate" });
            }
        }
    }
}