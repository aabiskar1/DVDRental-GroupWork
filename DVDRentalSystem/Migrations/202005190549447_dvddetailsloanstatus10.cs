namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DVDDetails", "AgeRestricted", c => c.Boolean(nullable: false));
            DropColumn("dbo.DVDDetails", "LoanStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DVDDetails", "LoanStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.DVDDetails", "AgeRestricted");
        }
    }
}
