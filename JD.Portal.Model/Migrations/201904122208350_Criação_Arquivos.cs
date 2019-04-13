namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_Arquivos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arquivos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataCriacao = c.DateTime(nullable: false),
                        Nome = c.String(nullable: false),
                        TamanhoBytes = c.Int(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50),
                        Atendimento_ID = c.Int(),
                        Projeto_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Atendimentos", t => t.Atendimento_ID)
                .ForeignKey("dbo.Projetos", t => t.Projeto_ID)
                .Index(t => t.Atendimento_ID)
                .Index(t => t.Projeto_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Arquivos", "Projeto_ID", "dbo.Projetos");
            DropForeignKey("dbo.Arquivos", "Atendimento_ID", "dbo.Atendimentos");
            DropIndex("dbo.Arquivos", new[] { "Projeto_ID" });
            DropIndex("dbo.Arquivos", new[] { "Atendimento_ID" });
            DropTable("dbo.Arquivos");
        }
    }
}
