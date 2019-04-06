namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remover_Cascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtualizacoesAtendimentos", "AtendimentoID", "dbo.Atendimentos");
            DropForeignKey("dbo.Atendimentos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.Atendimentos", "PessoaID", "dbo.Pessoas");
            DropForeignKey("dbo.AtualizacoesProjetos", "ProjetoID", "dbo.Projetos");
            DropForeignKey("dbo.Projetos", "BeneficiarioProjetoID", "dbo.BeneficiariosProjetos");
            DropForeignKey("dbo.Projetos", "DiaconoID", "dbo.Diaconos");
            AddForeignKey("dbo.AtualizacoesAtendimentos", "AtendimentoID", "dbo.Atendimentos", "ID");
            AddForeignKey("dbo.Atendimentos", "DiaconoID", "dbo.Diaconos", "ID");
            AddForeignKey("dbo.Atendimentos", "PessoaID", "dbo.Pessoas", "ID");
            AddForeignKey("dbo.AtualizacoesProjetos", "ProjetoID", "dbo.Projetos", "ID");
            AddForeignKey("dbo.Projetos", "BeneficiarioProjetoID", "dbo.BeneficiariosProjetos", "ID");
            AddForeignKey("dbo.Projetos", "DiaconoID", "dbo.Diaconos", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projetos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.Projetos", "BeneficiarioProjetoID", "dbo.BeneficiariosProjetos");
            DropForeignKey("dbo.AtualizacoesProjetos", "ProjetoID", "dbo.Projetos");
            DropForeignKey("dbo.Atendimentos", "PessoaID", "dbo.Pessoas");
            DropForeignKey("dbo.Atendimentos", "DiaconoID", "dbo.Diaconos");
            DropForeignKey("dbo.AtualizacoesAtendimentos", "AtendimentoID", "dbo.Atendimentos");
            AddForeignKey("dbo.Projetos", "DiaconoID", "dbo.Diaconos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Projetos", "BeneficiarioProjetoID", "dbo.BeneficiariosProjetos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AtualizacoesProjetos", "ProjetoID", "dbo.Projetos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Atendimentos", "PessoaID", "dbo.Pessoas", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Atendimentos", "DiaconoID", "dbo.Diaconos", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AtualizacoesAtendimentos", "AtendimentoID", "dbo.Atendimentos", "ID", cascadeDelete: true);
        }
    }
}
