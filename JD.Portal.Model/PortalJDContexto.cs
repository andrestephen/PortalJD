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

        DbSet<Pessoa> Pessoa { get; set; }
        DbSet<Diacono> Diacono { get; set; }
        DbSet<Atendimento> Atendimento { get; set; }
    }
}
