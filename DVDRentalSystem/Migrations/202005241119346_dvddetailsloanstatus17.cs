namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loans", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
