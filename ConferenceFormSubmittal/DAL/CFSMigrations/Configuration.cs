namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using ConferenceFormSubmittal.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ConferenceFormSubmittal.DAL.CFSEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\CFSMigrations";
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception e)
            {
                throw new Exception(
                     "Seed Failed - errors follow:\n" +
                     e.InnerException.InnerException.Message.ToString(), e
                 ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(ConferenceFormSubmittal.DAL.CFSEntities context)
        {
            //  This method will be called after migrating to the latest version.

            var statuses = new List<Status>
            {
                new Status {Description="Approved"},
                new Status {Description="Not Approved"},
                new Status {Description="Very Approved"}
            };
            statuses.ForEach(a => context.Statuses.AddOrUpdate(n => n.Description, a));
            SaveChanges(context);

            var employees = new List<Employee>
            {
                new Employee { FirstName = "Fred",   LastName = "Flintstone",  Email="fflintstone@outlook.com", Position="Teacher" , Location="St. Paul"  },
                new Employee { FirstName = "Wilma",   LastName = "Flintstone",  Email="wflintstone@outlook.com", Position="Teacher" , Location="St. Paul"},
                new Employee { FirstName = "Barney", LastName = "Rubble",  Email="brubble@outlook.com",  Position="Teacher" , Location="St. Paul" },
                new Employee { FirstName = "John",   LastName = "Smith", Email="jjs@outlook.com", Position="Teacher" , Location="St. Paul"}
            };
            employees.ForEach(a => context.Employees.AddOrUpdate(n => n.Email, a));
            SaveChanges(context);

            var conferences = new List<Conference>
            {
                new Conference { Name = "Pencil Pushing 101",   Location = "Toronto",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-10-20"), EndDate=DateTime.Parse("2018-10-22")  },
                new Conference { Name = "Math Expo",   Location = "Kingston",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-11-20"), EndDate=DateTime.Parse("2018-11-22")  },
                new Conference { Name = "Shapes and Lines",   Location = "Niagara Falls",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-6-20"), EndDate=DateTime.Parse("2018-6-22")  }
            };
            conferences.ForEach(a => context.Conferences.AddOrUpdate(n => n.Name, a));
            SaveChanges(context);

            var applications = new List<Application>
            {
                new Application {Rationale="Need to learn about pencils", ReplStaffReq=true, BudgetCode="12345",
                    DateSubmitted =DateTime.Parse("2018-2-7"), Feedback="Very usefull", EmployeeID=1, ConferenceID=1, StatusID=1  },
                new Application {Rationale="Need to learn about pencils", ReplStaffReq=true, BudgetCode="12345",
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very usefull", EmployeeID=2, ConferenceID=2, StatusID=2  },
                new Application {Rationale="Need to learn about pencils", ReplStaffReq=true, BudgetCode="12345",
                    DateSubmitted =DateTime.Parse("2018-2-7"), Feedback="Very usefull", EmployeeID=3, ConferenceID=3, StatusID=3  },
            };
            applications.ForEach(a => context.Applications.AddOrUpdate(n => n.Rationale, a));
            SaveChanges(context);

            var expenseTypes = new List<ExpenseType>
            {
                new ExpenseType {Description="Food"},
                new ExpenseType {Description="Hotel"},
                new ExpenseType {Description="Limo"}
            };
            expenseTypes.ForEach(a => context.ExpenseTypes.AddOrUpdate(n => n.Description, a));
            SaveChanges(context);

            var expenses = new List<Expense>
            {
                new Expense {Rationale="Need to learn about pencils", EstimatedCost=200, ActualCost=180, Feedback="good work",
                    ExpenseTypeID=1, StatusID=1, ApplicationID=1 },
                new Expense {Rationale="Need to learn about pencils", EstimatedCost=20, ActualCost=18, Feedback="good work",
                    ExpenseTypeID=2, StatusID=2, ApplicationID=1 },
                new Expense {Rationale="Need to learn about pencils", EstimatedCost=200, ActualCost=180, Feedback="good work",
                    ExpenseTypeID=3, StatusID=3, ApplicationID=2 },
            };
            expenses.ForEach(a => context.Expenses.AddOrUpdate(n => n.Rationale, a));
            SaveChanges(context);        

            var mileages = new List<Mileage>
            {
                new Mileage {TravelDate=DateTime.Parse("2018-2-7"), StartAddress="123 drive road", EndAddress="456 long parkway",
                    Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=1, ApplicationID=1},
                new Mileage {TravelDate=DateTime.Parse("2018-10-7"), StartAddress="11 white water road", EndAddress="321 oak drive",
                    Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=2, ApplicationID=2},
                new Mileage {TravelDate=DateTime.Parse("2018-2-7"), StartAddress="432 paved road", EndAddress="789 joke road",
                    Kilometres =60, Feedback="okay lets go", StatusID=3, EmployeeID=3, ApplicationID=3}
            };
            mileages.ForEach(a => context.Mileages.AddOrUpdate(n => n.StartAddress, a));
            SaveChanges(context);

            var sites = new List<Site>
            {
                new Site {Name="Air Canada", Address="123 main street"},
                new Site {Name="Toronto Conference Center", Address="700 conference parkway"}
            };
            sites.ForEach(a => context.Sites.AddOrUpdate(n => n.Name, a));
            SaveChanges(context);





        }
    }
}
