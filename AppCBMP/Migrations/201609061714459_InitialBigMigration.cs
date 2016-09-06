namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBigMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pesel = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthPlace = c.String(),
                        PostCode = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.Int(nullable: false),
                        Education = c.String(),
                        PhoneNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referrals",
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
                        CompanyId = c.Int(),
                        ReferralId = c.Int(),
                        TypeId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Referrals", t => t.ReferralId, cascadeDelete: true)
                .ForeignKey("dbo.PsychologicalExaminationTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.CompanyId)
                .Index(t => t.ReferralId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PsychologicalExaminationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonCompanies",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Company_Id })
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.ReferralPersons",
                c => new
                    {
                        Referral_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Referral_Id, t.Person_Id })
                .ForeignKey("dbo.Referrals", t => t.Referral_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Referral_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.PositionPsychologicalExaminations",
                c => new
                    {
                        Position_Id = c.Int(nullable: false),
                        PsychologicalExamination_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_Id, t.PsychologicalExamination_Id })
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.PsychologicalExamination_Id, cascadeDelete: true)
                .Index(t => t.Position_Id)
                .Index(t => t.PsychologicalExamination_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "TypeId", "dbo.PsychologicalExaminationTypes");
            DropForeignKey("dbo.Services", "ReferralId", "dbo.Referrals");
            DropForeignKey("dbo.PositionPsychologicalExaminations", "PsychologicalExamination_Id", "dbo.Services");
            DropForeignKey("dbo.PositionPsychologicalExaminations", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.Services", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Services", "PersonId", "dbo.People");
            DropForeignKey("dbo.ReferralPersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.ReferralPersons", "Referral_Id", "dbo.Referrals");
            DropForeignKey("dbo.PersonCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.PersonCompanies", "Person_Id", "dbo.People");
            DropIndex("dbo.PositionPsychologicalExaminations", new[] { "PsychologicalExamination_Id" });
            DropIndex("dbo.PositionPsychologicalExaminations", new[] { "Position_Id" });
            DropIndex("dbo.ReferralPersons", new[] { "Person_Id" });
            DropIndex("dbo.ReferralPersons", new[] { "Referral_Id" });
            DropIndex("dbo.PersonCompanies", new[] { "Company_Id" });
            DropIndex("dbo.PersonCompanies", new[] { "Person_Id" });
            DropIndex("dbo.Services", new[] { "TypeId" });
            DropIndex("dbo.Services", new[] { "ReferralId" });
            DropIndex("dbo.Services", new[] { "CompanyId" });
            DropIndex("dbo.Services", new[] { "PersonId" });
            DropTable("dbo.PositionPsychologicalExaminations");
            DropTable("dbo.ReferralPersons");
            DropTable("dbo.PersonCompanies");
            DropTable("dbo.PsychologicalExaminationTypes");
            DropTable("dbo.Positions");
            DropTable("dbo.Services");
            DropTable("dbo.Referrals");
            DropTable("dbo.People");
            DropTable("dbo.Companies");
        }
    }
}
