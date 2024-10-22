namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCreatedDateInReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "CreatedDate");
        }
    }
}
