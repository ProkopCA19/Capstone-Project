namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
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
            CreateIndex("dbo.Photographers", "ImageId");
            AddForeignKey("dbo.Photographers", "ImageId", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photographers", "ImageId", "dbo.Images");
            DropIndex("dbo.Photographers", new[] { "ImageId" });
            DropColumn("dbo.Photographers", "ImageId");
            DropTable("dbo.Images");
        }
    }
}
