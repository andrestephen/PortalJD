namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_Visitas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visitas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataVisita = c.DateTime(nullable: false),
                        LocalVisita = c.String(nullable: false),
                        NomeVisitado = c.String(nullable: false, maxLength: 100),
                        Resumo = c.String(nullable: false),
                        DiaconoID = c.Int(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoID)
                .Index(t => t.DiaconoID);
            
            CreateTable(
                "dbo.VisitasDiaconos",
                c => new
                    {
                        VisitaId = c.Int(nullable: false),
                        DiaconoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VisitaId, t.DiaconoId })
                .ForeignKey("dbo.Visitas", t => t.VisitaId, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoId, cascadeDelete: true)
                .Index(t => t.VisitaId)
                .Index(t => t.DiaconoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitasDiaconos", "DiaconoId", "dbo.Diaconos");
            DropForeignKey("dbo.VisitasDiaconos", "VisitaId", "dbo.Visitas");
            DropForeignKey("dbo.Visitas", "DiaconoID", "dbo.Diaconos");
            DropIndex("dbo.VisitasDiaconos", new[] { "DiaconoId" });
            DropIndex("dbo.VisitasDiaconos", new[] { "VisitaId" });
            DropIndex("dbo.Visitas", new[] { "DiaconoID" });
            DropTable("dbo.VisitasDiaconos");
            DropTable("dbo.Visitas");
        }
    }
}
