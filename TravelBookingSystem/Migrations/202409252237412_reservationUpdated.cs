namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "NumberOfAdults", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "NumberOfChildren", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Reservations", "NumberOfPeople");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "NumberOfPeople", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Reservations", "NumberOfChildren");
            DropColumn("dbo.Reservations", "NumberOfAdults");
        }
    }
}
