namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedFirstNameModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "FirstNameId", "dbo.FirstNames");
            DropIndex("dbo.People", new[] { "FirstNameId" });
            AddColumn("dbo.People", "FirstName", c => c.String());
            DropColumn("dbo.People", "FirstNameId");
            DropTable("dbo.FirstNames");
        }
        
        public override void Down()
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
            DropColumn("dbo.People", "FirstName");
            CreateIndex("dbo.People", "FirstNameId");
            AddForeignKey("dbo.People", "FirstNameId", "dbo.FirstNames", "Id", cascadeDelete: true);
        }
    }
}
