namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionar_Diacono_No_AppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DiaconoID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "DiaconoID");
            AddForeignKey("dbo.AspNetUsers", "DiaconoID", "dbo.Diaconos", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DiaconoID", "dbo.Diaconos");
            DropIndex("dbo.AspNetUsers", new[] { "DiaconoID" });
            DropColumn("dbo.AspNetUsers", "DiaconoID");
        }
    }
}
