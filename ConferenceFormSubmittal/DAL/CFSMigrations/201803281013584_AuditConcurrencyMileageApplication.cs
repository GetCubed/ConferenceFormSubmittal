namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditConcurrencyMileageApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Application", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Application", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Application", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Application", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Mileage", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Mileage", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Mileage", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Mileage", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Mileage", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mileage", "RowVersion");
            DropColumn("dbo.Mileage", "UpdatedOn");
            DropColumn("dbo.Mileage", "UpdatedBy");
            DropColumn("dbo.Mileage", "CreatedOn");
            DropColumn("dbo.Mileage", "CreatedBy");
            DropColumn("dbo.Application", "RowVersion");
            DropColumn("dbo.Application", "UpdatedOn");
            DropColumn("dbo.Application", "UpdatedBy");
            DropColumn("dbo.Application", "CreatedOn");
            DropColumn("dbo.Application", "CreatedBy");
        }
    }
}
