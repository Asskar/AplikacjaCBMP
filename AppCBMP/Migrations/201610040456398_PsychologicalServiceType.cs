namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PsychologicalServiceType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PsychologicalServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PsychologicalServices", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.PsychologicalServices", "PsychologicalServiceTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Positions", "ReportType", c => c.Byte(nullable: false));
            AlterColumn("dbo.PsychologicalServices", "Price", c => c.Double());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            CreateIndex("dbo.PsychologicalServices", "PsychologicalServiceTypeId");
            AddForeignKey("dbo.PsychologicalServices", "PsychologicalServiceTypeId", "dbo.PsychologicalServiceTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PsychologicalServices", "PsychologicalServiceTypeId", "dbo.PsychologicalServiceTypes");
            DropIndex("dbo.PsychologicalServices", new[] { "PsychologicalServiceTypeId" });
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.PsychologicalServices", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Positions", "ReportType");
            DropColumn("dbo.PsychologicalServices", "PsychologicalServiceTypeId");
            DropColumn("dbo.PsychologicalServices", "Number");
            DropTable("dbo.PsychologicalServiceTypes");
        }
    }
}
