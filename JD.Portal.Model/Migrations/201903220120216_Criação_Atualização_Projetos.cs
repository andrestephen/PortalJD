namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_Atualização_Projetos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AtualizacoesProjetos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataAtualizacao = c.DateTime(nullable: false),
                        DescricaoAtualizacao = c.String(nullable: false),
                        ProjetoID = c.Int(nullable: false),
                        DiaconoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projetos", t => t.ProjetoID, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoID)
                .Index(t => t.ProjetoID)
                .Index(t => t.DiaconoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AtualizacoesProjetos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.AtualizacoesProjetos", "ProjetoID", "dbo.Projetos");
            DropIndex("dbo.AtualizacoesProjetos", new[] { "DiaconoID" });
            DropIndex("dbo.AtualizacoesProjetos", new[] { "ProjetoID" });
            DropTable("dbo.AtualizacoesProjetos");
        }
    }
}
