namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PsychologistModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Psychologists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Initials = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PsychologicalServices", "PsychologistId", c => c.Int(nullable: false));
            CreateIndex("dbo.PsychologicalServices", "PsychologistId");
            AddForeignKey("dbo.PsychologicalServices", "PsychologistId", "dbo.Psychologists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PsychologicalServices", "PsychologistId", "dbo.Psychologists");
            DropIndex("dbo.PsychologicalServices", new[] { "PsychologistId" });
            DropColumn("dbo.PsychologicalServices", "PsychologistId");
            DropTable("dbo.Psychologists");
        }
    }
}
