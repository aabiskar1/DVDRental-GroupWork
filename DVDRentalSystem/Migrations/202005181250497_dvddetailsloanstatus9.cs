namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "CopyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "CopyId");
        }
    }
}
