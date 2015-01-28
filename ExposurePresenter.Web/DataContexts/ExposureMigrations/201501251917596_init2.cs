namespace ExposurePresenter.Web.DataContexts.ExposureMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExposureRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discipline = c.String(nullable: false, maxLength: 255),
                        Brand = c.String(nullable: false, maxLength: 255),
                        Branch = c.String(maxLength: 255),
                        Month = c.String(nullable: false, maxLength: 255),
                        Quantity = c.Long(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Brand, t.Month }, unique: true, name: "IX_BrandAndMonth");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExposureRecords", "IX_BrandAndMonth");
            DropTable("dbo.ExposureRecords");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Brands");
            DropTable("dbo.Branches");
        }
    }
}
