namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Atualizacao_Atendimento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AtualizacoesAtendimentos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataAtualizacao = c.DateTime(nullable: false),
                        DescricaoAtualizacao = c.String(nullable: false),
                        AtendimentoID = c.Int(nullable: false),
                        DiaconoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Atendimentos", t => t.AtendimentoID, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoID)
                .Index(t => t.AtendimentoID)
                .Index(t => t.DiaconoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AtualizacoesAtendimentos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.AtualizacoesAtendimentos", "AtendimentoID", "dbo.Atendimentos");
            DropIndex("dbo.AtualizacoesAtendimentos", new[] { "DiaconoID" });
            DropIndex("dbo.AtualizacoesAtendimentos", new[] { "AtendimentoID" });
            DropTable("dbo.AtualizacoesAtendimentos");
        }
    }
}
