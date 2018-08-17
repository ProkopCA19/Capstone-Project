namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "file", c => c.String());
            DropColumn("dbo.Images", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImagePath", c => c.String());
            DropColumn("dbo.Images", "file");
        }
    }
}
