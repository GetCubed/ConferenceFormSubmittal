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

    internal sealed class Configuration : DbMigrationsConfiguration<CFSEntities>
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

        protected override void Seed(CFSEntities context)
        {
            //  This method will be called after migrating to the latest version.

            var statuses = new List<Status>
            {
                new Status {Description="Submitted"},
                new Status {Description="Approved"},
                new Status {Description="Denied"},
                new Status {Description="Draft"}
            };
            statuses.ForEach(a => context.Statuses.AddOrUpdate(n => n.Description, a));
            SaveChanges(context);

            var employees = new List<Employee>
            {
                new Employee { FirstName = "Fred",   LastName = "Flintstone",  Email="fflintstone@outlook.com", Position="Teacher" , Location="St. Paul"  },
                new Employee { FirstName = "John",   LastName = "Bookman",  Email="jbookman@outlook.com", Position="Librarian" , Location="St. Paul"  },
                new Employee { FirstName = "Bob",   LastName = "Vance",  Email="bvance@outlook.com", Position="Principal" , Location="St. Michaels"  },
                new Employee { FirstName = "Sarah",   LastName = "Bookman",  Email="sbookman@outlook.com", Position="Librarian" , Location="St. Michaels"  },
                new Employee { FirstName = "Amy",   LastName = "Zetter",  Email="azetter@outlook.com", Position="Principal" , Location="St. Francis"  },
                new Employee { FirstName = "Wilma",   LastName = "Flintstone",  Email="wflintstone@outlook.com", Position="Teacher" , Location="St. Paul"},
                new Employee { FirstName = "Chris",   LastName = "Servos",  Email="cservos@outlook.com", Position="Head of Mathematics" , Location="St. Francis"},
                new Employee { FirstName = "Remylee",   LastName = "Agbuya",  Email="ragbuya@outlook.com", Position="Head of Literature" , Location="Mary Ward"},
                new Employee { FirstName = "Barney", LastName = "Rubble",  Email="brubble@outlook.com",  Position="Teacher" , Location="St. Paul" },
                new Employee { FirstName = "Cassidy", LastName = "Neil",  Email="cneil@outlook.com",  Position="Music Instructor" , Location="Mary ward" },
                new Employee { FirstName = "Elizabeth", LastName = "Reed",  Email="ereed@outlook.com",  Position="Librarian" , Location="St. Paul" },
                new Employee { FirstName = "Scarlet", LastName = "Begonias",  Email="sbegonias@outlook.com",  Position="Librarian" , Location="Notre Dame" },
                new Employee { FirstName = "Jack", LastName = "Straw",  Email="jstraw@outlook.com",  Position="Music Instructor" , Location="Notre Dame" },
                new Employee { FirstName = "John",   LastName = "Smith", Email="jjs@outlook.com", Position="Teacher" , Location="St. Paul"}
            };
            employees.ForEach(a => context.Employees.AddOrUpdate(n => n.Email, a));
            SaveChanges(context);

            var conferences = new List<Conference>
            {
                new Conference { Name = "Pencil Pushing 101",   Location = "Toronto",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-10-20"), EndDate=DateTime.Parse("2018-10-22")  },

                new Conference { Name = "Kingston Math Expo 2018",   Location = "Kingston",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-11-20"), EndDate=DateTime.Parse("2018-11-22")  },

                new Conference { Name = "Intel Technology",   Location = "Orlando",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-7-20"), EndDate=DateTime.Parse("2018-7-24")  },

                new Conference { Name = "History Extended",   Location = "Washington",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-2-20"), EndDate=DateTime.Parse("2018-2-22")  },

                new Conference { Name = "Time Managment Made Easy",   Location = "Toronto",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-4-20"), EndDate=DateTime.Parse("2018-4-22")  },

                new Conference { Name = "New Chemistry Instruction & Technique",   Location = "Montreal",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-5-20"), EndDate=DateTime.Parse("2018-5-22")  },

                new Conference { Name = "Better Understanding of Literature",   Location = "Hamilton",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-8-20"), EndDate=DateTime.Parse("2018-8-22")  },

                new Conference { Name = "Shakespeare to Understandable English",   Location = "London",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-8-14"), EndDate=DateTime.Parse("2018-8-18")  },

                new Conference { Name = "Guide to Better Leadership",   Location = "Windsor",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-5-6"), EndDate=DateTime.Parse("2018-5-9")  },

                new Conference { Name = "Nutrition for Learning 2.0",   Location = "Toronto",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-4-6"), EndDate=DateTime.Parse("2018-4-9")  },

                new Conference { Name = "Nashville Music Conference 2018",   Location = "Nashville",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-6-6"), EndDate=DateTime.Parse("2018-6-10")  },

                new Conference { Name = "Shapes & Lines",   Location = "Niagara Falls",  RegistrationCost=100,
                    StartDate =DateTime.Parse("2018-6-20"), EndDate=DateTime.Parse("2018-6-22")  }
            };
            conferences.ForEach(a => context.Conferences.AddOrUpdate(n => n.Name, a));
            SaveChanges(context);

            var paymentTypes = new List<PaymentType>
            {
                new PaymentType {Description="Corporate Credit Card"},
                new PaymentType {Description="Union Reimbursement"},
                new PaymentType {Description="Corporate Cheque Requisition"},
                new PaymentType {Description="Paid By Staff Member"}
            };
            paymentTypes.ForEach(a => context.PaymentTypes.AddOrUpdate(n => n.Description, a));
            SaveChanges(context);

            var applications = new List<Application>
            {
                new Application {Rationale="Pencil pushing is my life's passion.", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-10-20"), ReturnDate =DateTime.Parse("2018-10-22"),
                    AttendStartDate =DateTime.Parse("2018-10-20"), AttendEndDate =DateTime.Parse("2018-10-22"),
                    DateSubmitted =DateTime.Parse("2018-2-7"),
                    EmployeeID =(context.Employees.Where(e => e.Email == "jstraw@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Pencil Pushing 101").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Submitted").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Corporate Credit Card").SingleOrDefault().ID)

                },
                new Application {Rationale="Need to learn about math.", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-11-20"), ReturnDate =DateTime.Parse("2018-11-22"),
                    AttendStartDate =DateTime.Parse("2018-11-20"), AttendEndDate =DateTime.Parse("2018-11-22"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "wflintstone@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Kingston Math Expo 2018").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Approved").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Corporate Cheque Requisition").SingleOrDefault().ID)
                },
                new Application {Rationale="Need to learn about Leadership.", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-5-6"), ReturnDate =DateTime.Parse("2018-5-9"),
                    AttendStartDate =DateTime.Parse("2018-5-6"), AttendEndDate =DateTime.Parse("2018-5-9"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "cservos@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Guide to Better Leadership").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Approved").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Union Reimbursement").SingleOrDefault().ID)
                },
                new Application {Rationale="Need to learn about Time Management.", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-4-18"), ReturnDate =DateTime.Parse("2018-4-24"),
                    AttendStartDate =DateTime.Parse("2018-4-20"), AttendEndDate =DateTime.Parse("2018-4-22"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "ragbuya@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Time Managment Made Easy").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Denied").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Corporate Credit Card").SingleOrDefault().ID)
                },
                new Application {Rationale="Need to learn about Music.", ReplStaffReq=false, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-6-6"), ReturnDate =DateTime.Parse("2018-6-9"),
                    AttendStartDate =DateTime.Parse("2018-6-6"), AttendEndDate =DateTime.Parse("2018-6-9"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "ereed@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Nashville Music Conference 2018").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Approved").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Paid By Staff Member").SingleOrDefault().ID)
                },
                new Application {Rationale="Would like to have a better understanding of Shakespeare.", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-8-14"), ReturnDate =DateTime.Parse("2018-8-18"),
                    AttendStartDate =DateTime.Parse("2018-8-14"), AttendEndDate =DateTime.Parse("2018-8-18"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "sbegonias@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Shakespeare to Understandable English").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Denied").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Paid By Staff Member").SingleOrDefault().ID)
                },
                new Application {Rationale="Would like to learn more about Chemistry.", ReplStaffReq=false, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-5-20"), ReturnDate =DateTime.Parse("2018-5-22"),
                    AttendStartDate =DateTime.Parse("2018-5-20"), AttendEndDate =DateTime.Parse("2018-5-22"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "cneil@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "New Chemistry Instruction & Technique").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Approved").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Corporate Credit Card").SingleOrDefault().ID)
                },
                new Application {Rationale="I love History!", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-2-22"), ReturnDate =DateTime.Parse("2018-2-22"),
                    AttendStartDate =DateTime.Parse("2018-2-20"), AttendEndDate =DateTime.Parse("2018-2-22"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "jbookman@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "History Extended").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Approved").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Paid By Staff Member").SingleOrDefault().ID)
                },
                new Application {Rationale="I need to know how to turn on the sound on my cell phone.", ReplStaffReq=false, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-7-20"), ReturnDate =DateTime.Parse("2018-7-24"),
                    AttendStartDate =DateTime.Parse("2018-7-20"), AttendEndDate =DateTime.Parse("2018-7-24"),
                    DateSubmitted =DateTime.Parse("2018-1-7"), Feedback="Very useful",
                    EmployeeID =(context.Employees.Where(e => e.Email == "azetter@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Intel Technology").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Submitted").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Paid By Staff Member").SingleOrDefault().ID)
                },
                new Application {Rationale="I'm a square", ReplStaffReq=true, BudgetCode="12345",
                    DepartureDate =DateTime.Parse("2018-6-20"), ReturnDate =DateTime.Parse("2018-6-22"),
                    AttendStartDate =DateTime.Parse("2018-6-20"), AttendEndDate =DateTime.Parse("2018-6-22"),
                    DateSubmitted =DateTime.Parse("2018-2-7"), Feedback="Not gonna happen",
                    EmployeeID =(context.Employees.Where(e => e.Email == "bvance@outlook.com").SingleOrDefault().ID),
                    ConferenceID=(context.Conferences.Where(c=>c.Name == "Shapes & Lines").SingleOrDefault().ID),
                    StatusID=(context.Statuses.Where(s => s.Description == "Submitted").SingleOrDefault().ID),
                    PaymentTypeID=(context.PaymentTypes.Where(w => w.Description == "Corporate Credit Card").SingleOrDefault().ID)
                }
            };
            applications.ForEach(a => context.Applications.AddOrUpdate(n => n.Rationale, a));
            SaveChanges(context);

            var expenseTypes = new List<ExpenseType>
            {
                new ExpenseType {Description="Food"},
                new ExpenseType {Description="Accomodation"},
                new ExpenseType {Description="Limo"},
                new ExpenseType {Description="Notebook"},
                new ExpenseType {Description="Very Specific Conference Clothing"},
                new ExpenseType {Description="Conference Kart"},
                new ExpenseType {Description="Audio Recorder"},
                new ExpenseType {Description="Taxi"},
                new ExpenseType {Description="Bus"},
                new ExpenseType {Description="Airfare"}
            };
            expenseTypes.ForEach(a => context.ExpenseTypes.AddOrUpdate(n => n.Description, a));
            SaveChanges(context);

            var expenses = new List<Expense>
            {
                new Expense {Rationale="I will get hungry.", EstimatedCost=200,
                    ExpenseTypeID =1, ApplicationID=1 },
                new Expense {Rationale="I need a place to stay.", EstimatedCost=20, ActualCost=18,
                    ExpenseTypeID=2, ApplicationID=3 },
                new Expense {Rationale="I'm a too important for a taxi.", EstimatedCost=200, ActualCost=180,
                    ExpenseTypeID=3, ApplicationID=2 },
                new Expense {Rationale="I need to write things down.", EstimatedCost=13,
                    ExpenseTypeID =4, ApplicationID=1 },
                new Expense {Rationale="We must meet the required dress code.", EstimatedCost=80, ActualCost=72,
                    ExpenseTypeID=5, ApplicationID=3 },
                new Expense {Rationale="I need a kart to carry things around in the conference.", EstimatedCost=30, ActualCost=49,
                    ExpenseTypeID=6, ApplicationID=2 },
                new Expense {Rationale="I need to record the conference speech.", EstimatedCost=50,
                    ExpenseTypeID =7, ApplicationID=1 },
                new Expense {Rationale="I need transportation (My car is in shop).", EstimatedCost=80, ActualCost=72,
                    ExpenseTypeID=8, ApplicationID=3 },
                new Expense {Rationale="A bus would be much cheeper.", EstimatedCost=150, ActualCost=140,
                    ExpenseTypeID=9, ApplicationID=2 },
                new Expense {Rationale="A flight would be much cheeper.", EstimatedCost=240,
                    ExpenseTypeID=10, ApplicationID=1 },

            };
            expenses.ForEach(a => context.Expenses.AddOrUpdate(n => n.Rationale, a));
            SaveChanges(context);

            var mileages = new List<Mileage>
            {
                new Mileage {TravelDate=DateTime.Parse("2018-10-20"), StartAddress="427 1Rice Rd, Welland, ON L3C 7C1", EndAddress="40 Bay St, Toronto, ON M5J 2X2",
                    RoundTrip = false, Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=13, ApplicationID=1},

                new Mileage {TravelDate=DateTime.Parse("2018-11-20"), StartAddress="427 R23ice Rd, Welland, ON L3C 7C1", EndAddress="421 Union St W, Kingston, ON K7L 3N6",
                    RoundTrip = true, Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=6, ApplicationID=2},

                new Mileage {TravelDate=DateTime.Parse("2018-5-6"), StartAddress="427 Ric3e Rd, Welland, ON L3C 7C1", EndAddress="3995 Geraedts Dr, Windsor, ON N9G 3C3",
                    RoundTrip = true, Kilometres =200, Feedback="okay lets go", StatusID=2, EmployeeID=7, ApplicationID=3},

                new Mileage {TravelDate=DateTime.Parse("2018-4-20"), StartAddress="427 Ric4e Rd, Welland, ON L3C 7C1", EndAddress="222 Bremner Blvd, Toronto, ON M5V 3L9",
                    RoundTrip = true, Kilometres =600, Feedback="okay lets go", StatusID=3, EmployeeID=8, ApplicationID=4},

                new Mileage {TravelDate=DateTime.Parse("2018-6-6"), StartAddress="427 Ri5ce Rd, Welland, ON L3C 7C1", EndAddress="2800 Opryland Dr, Nashville, TN 37214, USA",
                    RoundTrip = false, Kilometres =60, Feedback="okay lets go", StatusID=4, EmployeeID=11, ApplicationID=5},

                new Mileage {TravelDate=DateTime.Parse("2018-8-14"), StartAddress="427 Rice6 Rd, Welland, ON L3C 7C1", EndAddress="300 York St, London, ON N6B 1P8",
                    RoundTrip = true, Kilometres =60, Feedback="okay lets go", StatusID=3, EmployeeID=12, ApplicationID=6},

                new Mileage {TravelDate=DateTime.Parse("2018-5-20"), StartAddress="427 Rice 7Rd, Welland, ON L3C 7C1", EndAddress="1001 Jean Paul Riopelle Pl, Montreal, QC H2Z 1H5",
                    RoundTrip = false, Kilometres =60, Feedback="okay lets go", StatusID=4, EmployeeID=10, ApplicationID=7},

                new Mileage {TravelDate=DateTime.Parse("2018-2-20"), StartAddress="427 Rice R8d, Welland, ON L3C 7C1", EndAddress="801 Mt Vernon Place NW, Washington, DC 20001, USA",
                    RoundTrip = false, Kilometres =60, Feedback="okay lets go", StatusID=2, EmployeeID=2, ApplicationID=8},

                new Mileage {TravelDate=DateTime.Parse("2018-7-20"), StartAddress="427 Rice R9d, Welland, ON L3C 7C1", EndAddress="9800 International Dr, Orlando, FL 32819, USA",
                    RoundTrip = true, Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=5, ApplicationID=9},

                new Mileage {TravelDate=DateTime.Parse("2018-6-20"), StartAddress="427 Rice Rd,0 Welland, ON L3C 7C1", EndAddress="6815 Stanley Ave, Niagara Falls, ON L2G 3Y9",
                    RoundTrip = true, Kilometres =60, Feedback="okay lets go", StatusID=1, EmployeeID=3, ApplicationID=10}
            };
            mileages.ForEach(a => context.Mileages.AddOrUpdate(n => n.StartAddress, a));
            SaveChanges(context);

            var sites = new List<Site>
            {
                new Site {Name="Air Canada Centre", Address="40 Bay St, Toronto, ON M5J 2X2"},
                new Site {Name="Hamilton Conference Centre", Address="1 Summers Ln, Hamilton, ON L8P 4Y2"},
                new Site {Name="Scotiabank Convention Centre", Address="6815 Stanley Ave, Niagara Falls, ON L2G 3Y9"},
                new Site {Name="London Convention Centre", Address="300 York St, London, ON N6B 1P8"},
                new Site {Name="Donald Gordon Convention Centre", Address="421 Union St W, Kingston, ON K7L 3N6"},
                new Site {Name="Palais des congrès de Montréal", Address="1001 Jean Paul Riopelle Pl, Montreal, QC H2Z 1H5"},
                new Site {Name="Walter E. Washington Convention Center", Address="801 Mt Vernon Place NW, Washington, DC 20001, USA"},
                new Site {Name="Gaylord Opryland Resort & Convention Center", Address="2800 Opryland Dr, Nashville, TN 37214, USA"},
                new Site {Name="Orange County Convention Center", Address="9800 International Dr, Orlando, FL 32819, USA"},
                new Site {Name="Residence & Conference Centre - Windsor", Address="3995 Geraedts Dr, Windsor, ON N9G 3C3"},
                new Site {Name="Metro Toronto Conference Center", Address="222 Bremner Blvd, Toronto, ON M5V 3L9"},

                new Site {Name="Saint Paul Catholic High School", Address="3834 Windermere Rd, Niagara Falls, ON L2J 2Y5"},
                new Site {Name="Saint Francis Catholic High School", Address="541 Lake St, St. Catharines, ON L2N 4H7"},
                new Site {Name="Mary Ward Catholic Elementary School", Address="2999 Dorchester Rd, Niagara Falls, ON L2J 2Z9"},
                new Site {Name="Notre Dame College School", Address="64 Smith St, Welland, ON L3C 4H4"},
                new Site {Name="Saint Michael Catholic High School", Address="8699 McLeod Rd, Niagara Falls, ON L2E 6S5"}

            };
            sites.ForEach(a => context.Sites.AddOrUpdate(n => n.Name, a));
            SaveChanges(context);
        }
    }
}
