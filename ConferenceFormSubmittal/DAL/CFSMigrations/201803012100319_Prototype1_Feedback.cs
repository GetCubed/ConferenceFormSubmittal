namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prototype1_Feedback : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Expense", new[] { "StatusID" });
            RenameColumn(table: "dbo.Expense", name: "StatusID", newName: "Status_ID");
            AddColumn("dbo.Application", "DepartureDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Application", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Application", "AttendStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Application", "AttendEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Mileage", "RoundTrip", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Expense", "Status_ID", c => c.Int());
            CreateIndex("dbo.Expense", "Status_ID");
            DropColumn("dbo.Expense", "Feedback");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expense", "Feedback", c => c.String(maxLength: 500));
            DropIndex("dbo.Expense", new[] { "Status_ID" });
            AlterColumn("dbo.Expense", "Status_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Mileage", "RoundTrip");
            DropColumn("dbo.Application", "AttendEndDate");
            DropColumn("dbo.Application", "AttendStartDate");
            DropColumn("dbo.Application", "ReturnDate");
            DropColumn("dbo.Application", "DepartureDate");
            RenameColumn(table: "dbo.Expense", name: "Status_ID", newName: "StatusID");
            CreateIndex("dbo.Expense", "StatusID");
        }
    }
}
