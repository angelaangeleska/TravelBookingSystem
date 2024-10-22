namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PackageId = c.Int(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PackageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "PackageId", "dbo.Packages");
            DropIndex("dbo.Reservations", new[] { "PackageId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropTable("dbo.Reservations");
        }
    }
}
