namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNameModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FirstNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.People", "FirstNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "FirstNameId");
            AddForeignKey("dbo.People", "FirstNameId", "dbo.FirstNames", "Id", cascadeDelete: true);
            DropColumn("dbo.People", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "FirstName", c => c.String());
            DropForeignKey("dbo.People", "FirstNameId", "dbo.FirstNames");
            DropIndex("dbo.People", new[] { "FirstNameId" });
            DropColumn("dbo.People", "FirstNameId");
            DropTable("dbo.FirstNames");
        }
    }
}
