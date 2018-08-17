namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photographers", "PhotoId", "dbo.Photos");
            DropIndex("dbo.Photographers", new[] { "PhotoId" });
            AddColumn("dbo.Photos", "Title", c => c.String());
            AddColumn("dbo.Photos", "PhotoPath", c => c.String());
            DropColumn("dbo.Photographers", "PhotoId");
            DropColumn("dbo.Photos", "ImagePath");
            DropColumn("dbo.Photos", "ImageMimeType");
            DropColumn("dbo.Photos", "ImageData");
            DropColumn("dbo.Photos", "ArtworkThumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "ArtworkThumbnail", c => c.Binary());
            AddColumn("dbo.Photos", "ImageData", c => c.Binary());
            AddColumn("dbo.Photos", "ImageMimeType", c => c.Int(nullable: false));
            AddColumn("dbo.Photos", "ImagePath", c => c.String());
            AddColumn("dbo.Photographers", "PhotoId", c => c.Int());
            DropColumn("dbo.Photos", "PhotoPath");
            DropColumn("dbo.Photos", "Title");
            CreateIndex("dbo.Photographers", "PhotoId");
            AddForeignKey("dbo.Photographers", "PhotoId", "dbo.Photos", "Id");
        }
    }
}
