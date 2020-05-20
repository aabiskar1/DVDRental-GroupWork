namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DVDDetails", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DVDDetails", "isDeleted");
        }
    }
}
