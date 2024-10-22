namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesMade : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accommodations", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Destinations", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Reservations", "UserEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "FullName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "FullName", c => c.String());
            AlterColumn("dbo.Reservations", "UserEmail", c => c.String());
            AlterColumn("dbo.Destinations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Accommodations", "Name", c => c.String(nullable: false));
        }
    }
}
