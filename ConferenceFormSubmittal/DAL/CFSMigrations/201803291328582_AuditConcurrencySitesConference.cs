namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditConcurrencySitesConference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conference", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Conference", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Conference", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Conference", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Conference", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Site", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Site", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Site", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Site", "UpdatedOn", c => c.DateTime());
            AddColumn("dbo.Site", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Site", "RowVersion");
            DropColumn("dbo.Site", "UpdatedOn");
            DropColumn("dbo.Site", "UpdatedBy");
            DropColumn("dbo.Site", "CreatedOn");
            DropColumn("dbo.Site", "CreatedBy");
            DropColumn("dbo.Conference", "RowVersion");
            DropColumn("dbo.Conference", "UpdatedOn");
            DropColumn("dbo.Conference", "UpdatedBy");
            DropColumn("dbo.Conference", "CreatedOn");
            DropColumn("dbo.Conference", "CreatedBy");
        }
    }
}
