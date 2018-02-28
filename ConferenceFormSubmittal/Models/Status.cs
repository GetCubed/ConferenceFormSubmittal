using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Status
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<Mileage> Mileages { get; set; }
    }
}