namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateReservationMoel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AddColumn("dbo.Reservations", "UserEmail", c => c.String());
            AddColumn("dbo.Reservations", "FullName", c => c.String());
            DropColumn("dbo.Reservations", "UserId");
            DropColumn("dbo.Reservations", "ReservationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "ReservationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Reservations", "FullName");
            DropColumn("dbo.Reservations", "UserEmail");
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
