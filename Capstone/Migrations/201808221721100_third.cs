namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ClientId", c => c.Int());
            AddColumn("dbo.Photographers", "AccountBalance", c => c.Double());
            CreateIndex("dbo.Events", "ClientId");
            AddForeignKey("dbo.Events", "ClientId", "dbo.Clients", "Id");
            DropColumn("dbo.Clients", "PriceRange1");
            DropColumn("dbo.Clients", "PriceRange2");
            DropColumn("dbo.Clients", "PriceRange3");
            DropColumn("dbo.Clients", "PriceRange4");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "PriceRange4", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "PriceRange3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "PriceRange2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "PriceRange1", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Events", "ClientId", "dbo.Clients");
            DropIndex("dbo.Events", new[] { "ClientId" });
            DropColumn("dbo.Photographers", "AccountBalance");
            DropColumn("dbo.Events", "ClientId");
        }
    }
}
