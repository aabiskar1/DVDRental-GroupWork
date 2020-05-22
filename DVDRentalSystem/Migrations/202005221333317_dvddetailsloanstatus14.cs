namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleViewModels", "test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleViewModels", "test");
        }
    }
}
