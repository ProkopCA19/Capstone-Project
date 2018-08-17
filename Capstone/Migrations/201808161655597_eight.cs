namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "ImageMimeType", c => c.Int(nullable: false));
            AddColumn("dbo.Photos", "ImageData", c => c.Binary());
            AddColumn("dbo.Photos", "ArtworkThumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "ArtworkThumbnail");
            DropColumn("dbo.Photos", "ImageData");
            DropColumn("dbo.Photos", "ImageMimeType");
        }
    }
}
