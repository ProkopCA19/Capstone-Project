namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photographers", "ImageId", "dbo.Images");
            DropIndex("dbo.Photographers", new[] { "ImageId" });
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Photographers", "PhotoId", c => c.Int());
            CreateIndex("dbo.Photographers", "PhotoId");
            AddForeignKey("dbo.Photographers", "PhotoId", "dbo.Photos", "Id");
            DropColumn("dbo.Photographers", "ImageId");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Photographers", "ImageId", c => c.Int());
            DropForeignKey("dbo.Photographers", "PhotoId", "dbo.Photos");
            DropIndex("dbo.Photographers", new[] { "PhotoId" });
            DropColumn("dbo.Photographers", "PhotoId");
            DropTable("dbo.Photos");
            CreateIndex("dbo.Photographers", "ImageId");
            AddForeignKey("dbo.Photographers", "ImageId", "dbo.Images", "Id");
        }
    }
}
