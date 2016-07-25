using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FamilyBilling.Models;

namespace FamilyBilling.DAL
{
    public class PersonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BillingContext>
    {
        protected override void Seed(BillingContext context)
        {
            var People = new List<Person>
            {
            new Person{Name="Gavin"},
            new Person{Name="Vania"},
            new Person{Name="Marshall"},
            new Person{Name="Simon"}
            };

            People.ForEach(s => context.People.Add(s));
            context.SaveChanges();
        }
    }
}