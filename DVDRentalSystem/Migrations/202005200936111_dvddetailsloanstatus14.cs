namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DVDDetails", "StockedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DVDDetails", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DVDDetails", "Name", c => c.String(nullable: false));
            DropColumn("dbo.DVDDetails", "StockedDate");
        }
    }
}
