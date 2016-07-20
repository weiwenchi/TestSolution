﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FamilyBilling.Models
{
    public class Billing
    {
        public int BillingID { get; set; }
        public float Amount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
        public string Comments { get; set; }
        public int PersonID { get; set; }
        public string IncludePersonIDs { get; set; }
        public string IncludePersonName { get; set; }
        public virtual Person Person
        { get; set; }

        public List<Person> IncludePeople { get; set; }
    }
}