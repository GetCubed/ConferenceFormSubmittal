namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableApplicationDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Application", "DepartureDate", c => c.DateTime());
            AlterColumn("dbo.Application", "ReturnDate", c => c.DateTime());
            AlterColumn("dbo.Application", "AttendStartDate", c => c.DateTime());
            AlterColumn("dbo.Application", "AttendEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Application", "AttendEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Application", "AttendStartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Application", "ReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Application", "DepartureDate", c => c.DateTime(nullable: false));
        }
    }
}
