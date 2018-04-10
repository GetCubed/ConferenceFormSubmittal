namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullablePaymentType : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Application", new[] { "PaymentTypeID" });
            AlterColumn("dbo.Application", "PaymentTypeID", c => c.Int());
            CreateIndex("dbo.Application", "PaymentTypeID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Application", new[] { "PaymentTypeID" });
            AlterColumn("dbo.Application", "PaymentTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Application", "PaymentTypeID");
        }
    }
}
