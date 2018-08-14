namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Zipcode", c => c.Int(nullable: false));
            AlterColumn("dbo.Photographers", "Zipcode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photographers", "Zipcode", c => c.String());
            AlterColumn("dbo.Clients", "Zipcode", c => c.String());
        }
    }
}
