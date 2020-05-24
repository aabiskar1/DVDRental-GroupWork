namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanTypes", "LoanDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanTypes", "LoanDays");
        }
    }
}
