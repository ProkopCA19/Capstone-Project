namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhotoURLPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "PhotoURLPath");
        }
    }
}
