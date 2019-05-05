namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criacao_Senha_Diacono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PerfisDiaconos",
                c => new
                    {
                        PerfilId = c.Int(nullable: false),
                        DiaconoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PerfilId, t.DiaconoId })
                .ForeignKey("dbo.Perfis", t => t.PerfilId, cascadeDelete: true)
                .ForeignKey("dbo.Diaconos", t => t.DiaconoId, cascadeDelete: true)
                .Index(t => t.PerfilId)
                .Index(t => t.DiaconoId);
            
            AddColumn("dbo.Diaconos", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Diaconos", "Senha", c => c.String(nullable: false));
            AddColumn("dbo.Diaconos", "DataCadastro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Diaconos", "Foto", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerfisDiaconos", "DiaconoId", "dbo.Diaconos");
            DropForeignKey("dbo.PerfisDiaconos", "PerfilId", "dbo.Perfis");
            DropIndex("dbo.PerfisDiaconos", new[] { "DiaconoId" });
            DropIndex("dbo.PerfisDiaconos", new[] { "PerfilId" });
            DropColumn("dbo.Diaconos", "Foto");
            DropColumn("dbo.Diaconos", "DataCadastro");
            DropColumn("dbo.Diaconos", "Senha");
            DropColumn("dbo.Diaconos", "Ativo");
            DropTable("dbo.PerfisDiaconos");
            DropTable("dbo.Perfis");
        }
    }
}
