namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tenth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhotographerId", c => c.Int());
            CreateIndex("dbo.Photos", "PhotographerId");
            AddForeignKey("dbo.Photos", "PhotographerId", "dbo.Photographers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "PhotographerId", "dbo.Photographers");
            DropIndex("dbo.Photos", new[] { "PhotographerId" });
            DropColumn("dbo.Photos", "PhotographerId");
        }
    }
}
