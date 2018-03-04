namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Application", "PaymentTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Application", "PaymentTypeID");
            AddForeignKey("dbo.Application", "PaymentTypeID", "dbo.PaymentType", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Application", "PaymentTypeID", "dbo.PaymentType");
            DropIndex("dbo.Application", new[] { "PaymentTypeID" });
            DropColumn("dbo.Application", "PaymentTypeID");
            DropTable("dbo.PaymentType");
        }
    }
}
