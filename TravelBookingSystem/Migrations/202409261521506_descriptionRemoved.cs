namespace TravelBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descriptionRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Description", c => c.String());
        }
    }
}
