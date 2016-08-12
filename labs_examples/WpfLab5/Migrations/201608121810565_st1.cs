namespace WpfLab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class st1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Number = c.Int(nullable: false),
                        AvgGrade = c.Double(nullable: false),
                        budgeted = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GroupID" });
            DropTable("dbo.Students");
            DropTable("dbo.Groups");
        }
    }
}
