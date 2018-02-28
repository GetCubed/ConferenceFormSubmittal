using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.Models
{
    public class ExpenseType
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}