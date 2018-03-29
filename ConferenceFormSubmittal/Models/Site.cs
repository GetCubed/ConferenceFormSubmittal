using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Site : Auditable
    {
        public int ID { get; set; }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "Site Name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Site Name must be between 5 and 100 characters in length.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100)]
        public string Address { get; set; }
    }
}