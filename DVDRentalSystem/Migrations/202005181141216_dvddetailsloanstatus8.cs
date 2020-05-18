namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "ActualReturnedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loans", "ActualReturnedDate", c => c.DateTime(nullable: false));
        }
    }
}
