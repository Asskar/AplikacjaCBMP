namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalizationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Localizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostCode = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PsychologicalServices", "LocalizationId", c => c.Int(nullable: false));
            CreateIndex("dbo.PsychologicalServices", "LocalizationId");
            AddForeignKey("dbo.PsychologicalServices", "LocalizationId", "dbo.Localizations", "Id", cascadeDelete: true);
            DropColumn("dbo.People", "ApartmentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "ApartmentNumber", c => c.Int());
            DropForeignKey("dbo.PsychologicalServices", "LocalizationId", "dbo.Localizations");
            DropIndex("dbo.PsychologicalServices", new[] { "LocalizationId" });
            DropColumn("dbo.PsychologicalServices", "LocalizationId");
            DropTable("dbo.Localizations");
        }
    }
}
