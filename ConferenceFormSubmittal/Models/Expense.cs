using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class Expense
    {
        public int ID { get; set; }

        [StringLength(500, ErrorMessage = "Rationale may include a maximum of 500 characters.")]
        public string Rationale { get; set; }

        [Display(Name = "Estimated Cost")]
        [DataType(DataType.Currency)]
        public decimal? EstimatedCost { get; set; }

        [Display(Name = "Actual Cost")]
        [DataType(DataType.Currency)]
        public decimal? ActualCost { get; set; }

        public int ExpenseTypeID { get; set; }

        public int ApplicationID { get; set; }

        public virtual ExpenseType ExpenseType { get; set; }

        public virtual Application Application { get; set; }
    }
}