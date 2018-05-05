using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
    }
}
