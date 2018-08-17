namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twelve : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stripes",
                c => new
                    {
                        stripePublishKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.stripePublishKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stripes");
        }
    }
}
