namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                        PriceRange1 = c.Boolean(nullable: false),
                        PriceRange2 = c.Boolean(nullable: false),
                        PriceRange3 = c.Boolean(nullable: false),
                        PriceRange4 = c.Boolean(nullable: false),
                        AppointmentId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AppointmentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Photographers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                        PriceRange1 = c.Boolean(nullable: false),
                        PriceRange2 = c.Boolean(nullable: false),
                        PriceRange3 = c.Boolean(nullable: false),
                        PriceRange4 = c.Boolean(nullable: false),
                        AppointmentId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.AppointmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AppointmentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photographers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Photographers", "AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.Photographers", new[] { "UserId" });
            DropIndex("dbo.Photographers", new[] { "AppointmentId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.Clients", new[] { "AppointmentId" });
            DropTable("dbo.Photographers");
            DropTable("dbo.Clients");
            DropTable("dbo.Appointments");
        }
    }
}
