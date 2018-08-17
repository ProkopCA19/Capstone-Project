namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eleventh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photographers", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photographers", "Bio");
        }
    }
}
