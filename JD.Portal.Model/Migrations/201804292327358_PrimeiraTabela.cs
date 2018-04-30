namespace JD.Portal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraTabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        RG = c.String(),
                        CPF = c.String(maxLength: 11),
                        Endereco = c.String(),
                        TelefonePrincipal = c.String(),
                        TelefoneSecundario = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pessoas");
        }
    }
}
