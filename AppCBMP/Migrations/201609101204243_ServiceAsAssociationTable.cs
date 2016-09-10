namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceAsAssociationTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonCompanies", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PersonCompanies", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ReferralPersons", "Referral_Id", "dbo.Referrals");
            DropForeignKey("dbo.ReferralPersons", "Person_Id", "dbo.People");
            DropIndex("dbo.Services", new[] { "CompanyId" });
            DropIndex("dbo.Services", new[] { "ReferralId" });
            DropIndex("dbo.PersonCompanies", new[] { "Person_Id" });
            DropIndex("dbo.PersonCompanies", new[] { "Company_Id" });
            DropIndex("dbo.ReferralPersons", new[] { "Referral_Id" });
            DropIndex("dbo.ReferralPersons", new[] { "Person_Id" });
            AlterColumn("dbo.Services", "CompanyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "ReferralId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "CompanyId");
            CreateIndex("dbo.Services", "ReferralId");
            DropTable("dbo.PersonCompanies");
            DropTable("dbo.ReferralPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReferralPersons",
                c => new
                    {
                        Referral_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Referral_Id, t.Person_Id });
            
            CreateTable(
                "dbo.PersonCompanies",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Company_Id });
            
            DropIndex("dbo.Services", new[] { "ReferralId" });
            DropIndex("dbo.Services", new[] { "CompanyId" });
            AlterColumn("dbo.Services", "ReferralId", c => c.Int());
            AlterColumn("dbo.Services", "CompanyId", c => c.Int());
            CreateIndex("dbo.ReferralPersons", "Person_Id");
            CreateIndex("dbo.ReferralPersons", "Referral_Id");
            CreateIndex("dbo.PersonCompanies", "Company_Id");
            CreateIndex("dbo.PersonCompanies", "Person_Id");
            CreateIndex("dbo.Services", "ReferralId");
            CreateIndex("dbo.Services", "CompanyId");
            AddForeignKey("dbo.ReferralPersons", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReferralPersons", "Referral_Id", "dbo.Referrals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonCompanies", "Company_Id", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonCompanies", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
