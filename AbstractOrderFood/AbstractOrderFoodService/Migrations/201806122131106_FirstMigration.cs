namespace AbstractOrderFoodService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomersId = c.Int(nullable: false),
                        CourseSetsId = c.Int(nullable: false),
                        ChefsId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chefs", t => t.ChefsId)
                .ForeignKey("dbo.CourseSets", t => t.CourseSetsId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomersId, cascadeDelete: true)
                .Index(t => t.CustomersId)
                .Index(t => t.CourseSetsId)
                .Index(t => t.ChefsId);
            
            CreateTable(
                "dbo.Chefs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChefsFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseSetsName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseSetsCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseSetsId = c.Int(nullable: false),
                        CoursesId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.CourseSets", t => t.CourseSetsId, cascadeDelete: true)
                .Index(t => t.CourseSetsId)
                .Index(t => t.CoursesId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoursesName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KitchenCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitchenId = c.Int(nullable: false),
                        CoursesId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.Kitchens", t => t.KitchenId, cascadeDelete: true)
                .Index(t => t.KitchenId)
                .Index(t => t.CoursesId);
            
            CreateTable(
                "dbo.Kitchens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitchenName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomersFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketCourses", "CustomersId", "dbo.Customers");
            DropForeignKey("dbo.CourseSetsCourses", "CourseSetsId", "dbo.CourseSets");
            DropForeignKey("dbo.KitchenCourses", "KitchenId", "dbo.Kitchens");
            DropForeignKey("dbo.KitchenCourses", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.CourseSetsCourses", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.BasketCourses", "CourseSetsId", "dbo.CourseSets");
            DropForeignKey("dbo.BasketCourses", "ChefsId", "dbo.Chefs");
            DropIndex("dbo.KitchenCourses", new[] { "CoursesId" });
            DropIndex("dbo.KitchenCourses", new[] { "KitchenId" });
            DropIndex("dbo.CourseSetsCourses", new[] { "CoursesId" });
            DropIndex("dbo.CourseSetsCourses", new[] { "CourseSetsId" });
            DropIndex("dbo.BasketCourses", new[] { "ChefsId" });
            DropIndex("dbo.BasketCourses", new[] { "CourseSetsId" });
            DropIndex("dbo.BasketCourses", new[] { "CustomersId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Kitchens");
            DropTable("dbo.KitchenCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseSetsCourses");
            DropTable("dbo.CourseSets");
            DropTable("dbo.Chefs");
            DropTable("dbo.BasketCourses");
        }
    }
}
