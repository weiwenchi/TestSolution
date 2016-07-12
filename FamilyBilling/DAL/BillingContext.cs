using FamilyBilling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FamilyBilling.DAL
{
    public class BillingContext : DbContext
    {
        public BillingContext() : base("BillingContext")
        {
        }

        public DbSet<Billing> Billings { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}