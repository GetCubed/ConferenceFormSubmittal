namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rationale = c.String(maxLength: 500),
                        ReplStaffReq = c.Boolean(nullable: false),
                        BudgetCode = c.String(),
                        DateSubmitted = c.DateTime(),
                        Feedback = c.String(maxLength: 500),
                        EmployeeID = c.Int(nullable: false),
                        ConferenceID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conference", t => t.ConferenceID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ConferenceID)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.Conference",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 100),
                        RegistrationCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Position = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Mileage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TravelDate = c.DateTime(nullable: false),
                        StartAddress = c.String(nullable: false, maxLength: 100),
                        EndAddress = c.String(nullable: false, maxLength: 100),
                        Kilometres = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Feedback = c.String(maxLength: 500),
                        StatusID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        ApplicationID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .Index(t => t.StatusID)
                .Index(t => t.EmployeeID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rationale = c.String(maxLength: 500),
                        EstimatedCost = c.Decimal(precision: 18, scale: 2),
                        ActualCost = c.Decimal(precision: 18, scale: 2),
                        Feedback = c.String(maxLength: 500),
                        ExpenseTypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.ExpenseType", t => t.ExpenseTypeID)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .Index(t => t.ExpenseTypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.ExpenseType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Site",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mileage", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Expense", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Expense", "ExpenseTypeID", "dbo.ExpenseType");
            DropForeignKey("dbo.Expense", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Application", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Mileage", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Mileage", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Application", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Application", "ConferenceID", "dbo.Conference");
            DropIndex("dbo.Expense", new[] { "ApplicationID" });
            DropIndex("dbo.Expense", new[] { "StatusID" });
            DropIndex("dbo.Expense", new[] { "ExpenseTypeID" });
            DropIndex("dbo.Mileage", new[] { "ApplicationID" });
            DropIndex("dbo.Mileage", new[] { "EmployeeID" });
            DropIndex("dbo.Mileage", new[] { "StatusID" });
            DropIndex("dbo.Application", new[] { "StatusID" });
            DropIndex("dbo.Application", new[] { "ConferenceID" });
            DropIndex("dbo.Application", new[] { "EmployeeID" });
            DropTable("dbo.Site");
            DropTable("dbo.ExpenseType");
            DropTable("dbo.Expense");
            DropTable("dbo.Status");
            DropTable("dbo.Mileage");
            DropTable("dbo.Employee");
            DropTable("dbo.Conference");
            DropTable("dbo.Application");
        }
    }
}
