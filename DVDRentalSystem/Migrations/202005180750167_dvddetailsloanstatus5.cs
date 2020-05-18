namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producers", "CastDetailsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Producers", "CastDetailsId");
            AddForeignKey("dbo.Producers", "CastDetailsId", "dbo.CastDetails", "CastDetailsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producers", "CastDetailsId", "dbo.CastDetails");
            DropIndex("dbo.Producers", new[] { "CastDetailsId" });
            DropColumn("dbo.Producers", "CastDetailsId");
        }
    }
}
