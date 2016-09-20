namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PsychologicalService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionPsychologicalServices",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        PsychologicalService_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.PsychologicalService_Id })
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.PsychologicalServices", t => t.PsychologicalService_Id, cascadeDelete: true)
                .Index(t => t.Position_Id)
                .Index(t => t.PsychologicalService_Id);
            
            AddColumn("dbo.PsychologicalServices", "DateTimeOfService", c => c.DateTime(nullable: false));
            AddColumn("dbo.PsychologicalServices", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.PsychologicalServices", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.PsychologicalServices", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.PsychologicalServices", "ReferralId", c => c.Int(nullable: false));
            CreateIndex("dbo.PsychologicalServices", "PersonId");
            CreateIndex("dbo.PsychologicalServices", "CompanyId");
            CreateIndex("dbo.PsychologicalServices", "ReferralId");
            AddForeignKey("dbo.PsychologicalServices", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PsychologicalServices", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PsychologicalServices", "ReferralId", "dbo.Referrals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PsychologicalServices", "ReferralId", "dbo.Referrals");
            DropForeignKey("dbo.PositionPsychologicalServices", "PsychologicalService_Id", "dbo.PsychologicalServices");
            DropForeignKey("dbo.PositionPsychologicalServices", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.PsychologicalServices", "PersonId", "dbo.People");
            DropForeignKey("dbo.PsychologicalServices", "CompanyId", "dbo.Companies");
            DropIndex("dbo.PositionPsychologicalServices", new[] { "PsychologicalService_Id" });
            DropIndex("dbo.PositionPsychologicalServices", new[] { "Position_Id" });
            DropIndex("dbo.PsychologicalServices", new[] { "ReferralId" });
            DropIndex("dbo.PsychologicalServices", new[] { "CompanyId" });
            DropIndex("dbo.PsychologicalServices", new[] { "PersonId" });
            DropColumn("dbo.PsychologicalServices", "ReferralId");
            DropColumn("dbo.PsychologicalServices", "CompanyId");
            DropColumn("dbo.PsychologicalServices", "PersonId");
            DropColumn("dbo.PsychologicalServices", "Price");
            DropColumn("dbo.PsychologicalServices", "DateTimeOfService");
            DropTable("dbo.PositionPsychologicalServices");
        }
    }
}
