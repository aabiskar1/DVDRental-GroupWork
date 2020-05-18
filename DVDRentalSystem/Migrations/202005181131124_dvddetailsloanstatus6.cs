namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "ActualReturnedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Loans", "returned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "returned", c => c.Boolean(nullable: false));
            DropColumn("dbo.Loans", "ActualReturnedDate");
        }
    }
}
