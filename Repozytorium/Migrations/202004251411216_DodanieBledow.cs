namespace Repozytorium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanieBledow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bledy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrescBledu = c.String(nullable: false, maxLength: 200),
                        DataDodania = c.DateTime(nullable: false),
                        UzytkownikId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UzytkownikId)
                .Index(t => t.UzytkownikId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bledy", "UzytkownikId", "dbo.AspNetUsers");
            DropIndex("dbo.Bledy", new[] { "UzytkownikId" });
            DropTable("dbo.Bledy");
        }
    }
}
