using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JD.Portal.Model
{
    public class PortalJDContexto : DbContext
    {
        public PortalJDContexto() : base("PortalJDContexto")
        {

        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Diacono> Diacono { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<AtualizacaoAtendimento> AtualizacaoAtendimento { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<AtualizacaoProjeto> AtualizacaoProjeto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Diacono>()
                .HasMany(d => d.AtualizacoesAtendimento)
                .WithRequired(a => a.Diacono)
                //.Map(m => {
                //    m.ToTable("UsersRoles");
                //    m.MapLeftKey("UserId");
                //    m.MapRightKey("RoleId");
                //})
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Diacono>()
                .HasMany(d => d.AtualizacoesProjeto)
                .WithRequired(a => a.Diacono)
                //.Map(m => {
                //    m.ToTable("UsersRoles");
                //    m.MapLeftKey("UserId");
                //    m.MapRightKey("RoleId");
                //})
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Projeto>()
               .HasMany(p => p.Diaconos)
               .WithMany(d => d.Projetos)
               .Map(pd =>
               {
                   pd.ToTable("ProjetosDiaconos");
                   pd.MapLeftKey("ProjetoId");
                   pd.MapRightKey("DiaconoId");
               });


            modelBuilder.Entity<Atendimento>()
               .HasMany(a => a.Diaconos)
               .WithMany(d => d.Atendimentos)
               .Map(pd =>
               {
                   pd.ToTable("AtendimentosDiaconos");
                   pd.MapLeftKey("AtendimentoId");
                   pd.MapRightKey("DiaconoId");
               });
        }


    }
}
