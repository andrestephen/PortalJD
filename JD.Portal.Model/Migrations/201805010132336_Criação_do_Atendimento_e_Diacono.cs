namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_do_Atendimento_e_Diacono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atendimentos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataAtendimento = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        PessoaID = c.Int(nullable: false),
                        DiaconoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoID, cascadeDelete: true)
                .ForeignKey("dbo.Pessoas", t => t.PessoaID, cascadeDelete: true)
                .Index(t => t.PessoaID)
                .Index(t => t.DiaconoID);
            
            CreateTable(
                "dbo.Diaconos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Telefone = c.String(),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Atendimentos", "PessoaID", "dbo.Pessoas");
            DropForeignKey("dbo.Atendimentos", "DiaconoID", "dbo.Diaconos");
            DropIndex("dbo.Atendimentos", new[] { "DiaconoID" });
            DropIndex("dbo.Atendimentos", new[] { "PessoaID" });
            DropTable("dbo.Diaconos");
            DropTable("dbo.Atendimentos");
        }
    }
}
