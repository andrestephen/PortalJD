namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_Responsaveis_Projeto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjetosDiaconos",
                c => new
                    {
                        ProjetoId = c.Int(nullable: false),
                        DiaconoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjetoId, t.DiaconoId })
                .ForeignKey("dbo.Projetos", t => t.ProjetoId, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoId, cascadeDelete: true)
                .Index(t => t.ProjetoId)
                .Index(t => t.DiaconoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjetosDiaconos", "DiaconoId", "dbo.Diaconos");
            DropForeignKey("dbo.ProjetosDiaconos", "ProjetoId", "dbo.Projetos");
            DropIndex("dbo.ProjetosDiaconos", new[] { "DiaconoId" });
            DropIndex("dbo.ProjetosDiaconos", new[] { "ProjetoId" });
            DropTable("dbo.ProjetosDiaconos");
        }
    }
}
