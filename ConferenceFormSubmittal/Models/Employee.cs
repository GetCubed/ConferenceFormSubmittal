using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Employee
    {
        public Employee()
        {
            Applications = new HashSet<Application>();
            Mileages = new HashSet<Mileage>();
        }

        public int ID { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public virtual ICollection<Mileage> Mileages { get; set; }
    }
}