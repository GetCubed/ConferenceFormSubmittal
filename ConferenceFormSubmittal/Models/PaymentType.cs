using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class PaymentType
    {
        public int ID { get; set; }

        [Display(Name = "Payment Type")]
        public string Description { get; set; }
    }
}