using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyBilling.Models
{
    public class Billing
    {
        public int BillingID { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int PersonID { get; set; }
        public string IncludePersonIDs { get; set; }

        public virtual Person Person
        { get; set; }
    }
}