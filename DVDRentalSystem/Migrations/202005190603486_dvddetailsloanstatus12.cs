namespace DVDRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dvddetailsloanstatus12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        LoanTypeId = c.Int(nullable: false, identity: true),
                        LoanTypeName = c.String(),
                    })
                .PrimaryKey(t => t.LoanTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanTypes");
        }
    }
}
