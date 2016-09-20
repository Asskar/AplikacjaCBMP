namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicesRemodeling : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Services", "PersonId", "dbo.People");
            DropForeignKey("dbo.Services", "ReferralId", "dbo.Referrals");
            DropForeignKey("dbo.PositionPsychologicalExaminations", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.PositionPsychologicalExaminations", "PsychologicalExamination_Id", "dbo.Services");
            DropForeignKey("dbo.Services", "TypeId", "dbo.PsychologicalExaminationTypes");
            DropIndex("dbo.Services", new[] { "PersonId" });
            DropIndex("dbo.Services", new[] { "CompanyId" });
            DropIndex("dbo.Services", new[] { "ReferralId" });
            DropIndex("dbo.Services", new[] { "TypeId" });
            DropIndex("dbo.PositionPsychologicalExaminations", new[] { "Position_Id" });
            DropIndex("dbo.PositionPsychologicalExaminations", new[] { "PsychologicalExamination_Id" });
            CreateTable(
                "dbo.PsychologicalServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Services");
            DropTable("dbo.PsychologicalExaminationTypes");
            DropTable("dbo.PositionPsychologicalExaminations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PositionPsychologicalExaminations",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        PsychologicalExamination_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.PsychologicalExamination_Id });
            
            CreateTable(
                "dbo.PsychologicalExaminationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTimeOfService = c.DateTime(nullable: false),
                        PersonId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        ReferralId = c.Int(nullable: false),
                        TypeId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PsychologicalServices");
            CreateIndex("dbo.PositionPsychologicalExaminations", "PsychologicalExamination_Id");
            CreateIndex("dbo.PositionPsychologicalExaminations", "Position_Id");
            CreateIndex("dbo.Services", "TypeId");
            CreateIndex("dbo.Services", "ReferralId");
            CreateIndex("dbo.Services", "CompanyId");
            CreateIndex("dbo.Services", "PersonId");
            AddForeignKey("dbo.Services", "TypeId", "dbo.PsychologicalExaminationTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PositionPsychologicalExaminations", "PsychologicalExamination_Id", "dbo.Services", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PositionPsychologicalExaminations", "Position_Id", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Services", "ReferralId", "dbo.Referrals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Services", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Services", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
