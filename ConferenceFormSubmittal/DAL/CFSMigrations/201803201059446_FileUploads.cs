namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileUploads : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documentation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fileName = c.String(maxLength: 256),
                        Description = c.String(maxLength: 256),
                        ExpenseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Expense", t => t.ExpenseID)
                .Index(t => t.ExpenseID);
            
            CreateTable(
                "dbo.FileContent",
                c => new
                    {
                        FileContentID = c.Int(nullable: false),
                        Content = c.Binary(),
                        MimeType = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FileContentID)
                .ForeignKey("dbo.Documentation", t => t.FileContentID)
                .Index(t => t.FileContentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileContent", "FileContentID", "dbo.Documentation");
            DropForeignKey("dbo.Documentation", "ExpenseID", "dbo.Expense");
            DropIndex("dbo.FileContent", new[] { "FileContentID" });
            DropIndex("dbo.Documentation", new[] { "ExpenseID" });
            DropTable("dbo.FileContent");
            DropTable("dbo.Documentation");
        }
    }
}
