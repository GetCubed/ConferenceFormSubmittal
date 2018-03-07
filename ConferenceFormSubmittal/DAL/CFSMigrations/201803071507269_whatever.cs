namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatever : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Expense", "EstimatedCost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Expense", "EstimatedCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
