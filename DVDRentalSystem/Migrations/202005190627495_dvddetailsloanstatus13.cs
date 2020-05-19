namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoanTypes", "LoanTypeName", c => c.String(nullable: false));
            CreateIndex("dbo.Loans", "LoanTypeId");
            AddForeignKey("dbo.Loans", "LoanTypeId", "dbo.LoanTypes", "LoanTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "LoanTypeId", "dbo.LoanTypes");
            DropIndex("dbo.Loans", new[] { "LoanTypeId" });
            AlterColumn("dbo.LoanTypes", "LoanTypeName", c => c.String());
        }
    }
}
