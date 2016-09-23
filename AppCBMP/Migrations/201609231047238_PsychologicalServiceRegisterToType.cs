namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PsychologicalServiceRegisterToType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PsychologicalServiceRegisters", newName: "PsychologicalServiceTypes");
            RenameColumn(table: "dbo.PsychologicalServices", name: "PsychologicalServiceRegisterId", newName: "PsychologicalServiceTypeId");
            RenameIndex(table: "dbo.PsychologicalServices", name: "IX_PsychologicalServiceRegisterId", newName: "IX_PsychologicalServiceTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PsychologicalServices", name: "IX_PsychologicalServiceTypeId", newName: "IX_PsychologicalServiceRegisterId");
            RenameColumn(table: "dbo.PsychologicalServices", name: "PsychologicalServiceTypeId", newName: "PsychologicalServiceRegisterId");
            RenameTable(name: "dbo.PsychologicalServiceTypes", newName: "PsychologicalServiceRegisters");
        }
    }
}
