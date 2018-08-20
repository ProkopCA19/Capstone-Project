namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "text", c => c.String());
            AddColumn("dbo.Appointments", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Appointments", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "EndDate");
            DropColumn("dbo.Appointments", "StartDate");
            DropColumn("dbo.Appointments", "text");
        }
    }
}
