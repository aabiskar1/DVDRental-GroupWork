namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoleViewModels", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleViewModels", "test", c => c.String());
        }
    }
}
