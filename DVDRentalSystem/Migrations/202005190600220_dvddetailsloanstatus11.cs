namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "LoanCharge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "LoanCharge");
        }
    }
}
