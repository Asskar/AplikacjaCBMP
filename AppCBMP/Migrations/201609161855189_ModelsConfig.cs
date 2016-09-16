namespace AppCBMP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.People", "Pesel", c => c.String(nullable: false));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.People", "BirthPlace", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.People", "PostCode", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.People", "City", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.People", "Street", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.People", "HouseNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.People", "ApartmentNumber", c => c.Int());
            AlterColumn("dbo.People", "Education", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.People", "PhoneNumber", c => c.Long());
            AlterColumn("dbo.Referrals", "Name", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Referrals", "Name", c => c.String());
            AlterColumn("dbo.People", "PhoneNumber", c => c.Long(nullable: false));
            AlterColumn("dbo.People", "Education", c => c.String());
            AlterColumn("dbo.People", "ApartmentNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "HouseNumber", c => c.String());
            AlterColumn("dbo.People", "Street", c => c.String());
            AlterColumn("dbo.People", "City", c => c.String());
            AlterColumn("dbo.People", "PostCode", c => c.String());
            AlterColumn("dbo.People", "BirthPlace", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
            AlterColumn("dbo.People", "Pesel", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String());
        }
    }
}
