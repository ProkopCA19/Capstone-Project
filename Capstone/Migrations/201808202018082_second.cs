namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photographers", "EventId", "dbo.Events");
            DropIndex("dbo.Photographers", new[] { "EventId" });
            AddColumn("dbo.Events", "PhotographerId", c => c.Int());
            CreateIndex("dbo.Events", "PhotographerId");
            AddForeignKey("dbo.Events", "PhotographerId", "dbo.Photographers", "Id");
            DropColumn("dbo.Photographers", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photographers", "EventId", c => c.Int());
            DropForeignKey("dbo.Events", "PhotographerId", "dbo.Photographers");
            DropIndex("dbo.Events", new[] { "PhotographerId" });
            DropColumn("dbo.Events", "PhotographerId");
            CreateIndex("dbo.Photographers", "EventId");
            AddForeignKey("dbo.Photographers", "EventId", "dbo.Events", "Id");
        }
    }
}
