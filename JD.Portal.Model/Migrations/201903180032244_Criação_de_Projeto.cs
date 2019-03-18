namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_de_Projeto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projetos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 250),
                        Descricao = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        BeneficiarioProjetoID = c.Int(nullable: false),
                        DiaconoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BeneficiariosProjetos", t => t.BeneficiarioProjetoID, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoID, cascadeDelete: true)
                .Index(t => t.BeneficiarioProjetoID)
                .Index(t => t.DiaconoID);
            
            CreateTable(
                "dbo.BeneficiariosProjetos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 250),
                        Endereco = c.String(),
                        NomeContato = c.String(maxLength: 100),
                        EmailContato = c.String(maxLength: 100),
                        TelefonePrincipal = c.String(),
                        TelefoneSecundario = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projetos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.Projetos", "BeneficiarioProjetoID", "dbo.BeneficiariosProjetos");
            DropIndex("dbo.Projetos", new[] { "DiaconoID" });
            DropIndex("dbo.Projetos", new[] { "BeneficiarioProjetoID" });
            DropTable("dbo.BeneficiariosProjetos");
            DropTable("dbo.Projetos");
        }
    }
}
