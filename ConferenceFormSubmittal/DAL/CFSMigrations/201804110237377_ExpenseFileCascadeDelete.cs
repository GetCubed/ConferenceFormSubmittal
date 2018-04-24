namespace ConferenceFormSubmittal.DAL.CFSMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseFileCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documentation", "ExpenseID", "dbo.Expense");
            DropForeignKey("dbo.FileContent", "FileContentID", "dbo.Documentation");
            AddForeignKey("dbo.Documentation", "ExpenseID", "dbo.Expense", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FileContent", "FileContentID", "dbo.Documentation", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileContent", "FileContentID", "dbo.Documentation");
            DropForeignKey("dbo.Documentation", "ExpenseID", "dbo.Expense");
            AddForeignKey("dbo.FileContent", "FileContentID", "dbo.Documentation", "ID");
            AddForeignKey("dbo.Documentation", "ExpenseID", "dbo.Expense", "ID");
        }
    }
}
