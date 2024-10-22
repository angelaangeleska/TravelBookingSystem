namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class packageUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Description");
        }
    }
}
