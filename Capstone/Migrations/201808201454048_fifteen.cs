namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "text");
            DropColumn("dbo.Appointments", "StartDate");
            DropColumn("dbo.Appointments", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "text", c => c.String());
            DropColumn("dbo.Appointments", "Date");
        }
    }
}
