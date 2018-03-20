using ConferenceFormSubmittal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ConferenceFormSubmittal.DAL
{
    public class CFSEntities : DbContext
    {
        public CFSEntities() : base("name=CFSEntities")
        {
            // SHOW ME THE MONEY... (Or at least the SQL)
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Mileage> Mileages { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Documentation> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            ////Added for cascade delete for all Files with Expense
            //modelBuilder.Entity<Expense>()
            //    .HasMany(a => a.Files)
            //    .WithRequired(p => p.Expense)
            //    .WillCascadeOnDelete(true);

            ////Added for cascade delete for FileContent with Documentation
            //modelBuilder.Entity<Documentation>()
            //    .HasOptional(w => w.FileContent)
            //    .WithRequired(p => p.Documentation)
            //    .WillCascadeOnDelete(true);
        }
    }
}