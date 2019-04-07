namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criação_Responsaveis_Atendimentos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AtendimentosDiaconos",
                c => new
                    {
                        AtendimentoId = c.Int(nullable: false),
                        DiaconoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AtendimentoId, t.DiaconoId })
                .ForeignKey("dbo.Atendimentos", t => t.AtendimentoId, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoId, cascadeDelete: true)
                .Index(t => t.AtendimentoId)
                .Index(t => t.DiaconoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AtendimentosDiaconos", "DiaconoId", "dbo.Diaconos");
            DropForeignKey("dbo.AtendimentosDiaconos", "AtendimentoId", "dbo.Atendimentos");
            DropIndex("dbo.AtendimentosDiaconos", new[] { "DiaconoId" });
            DropIndex("dbo.AtendimentosDiaconos", new[] { "AtendimentoId" });
            DropTable("dbo.AtendimentosDiaconos");
        }
    }
}
