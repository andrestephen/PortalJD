using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSDiacono
    {
        public bool AutenticarUsuario(string email, string senha)
        {
            bool autenticado = false;

            using (var db = new PortalJDContexto())
            {
                autenticado = (from d in db.Diacono
                               where d.Email == email && d.Senha == senha
                               select d).Count() > 0;
            }

            return autenticado;
        }

        public Diacono RecuperarDiaconoPorEmail(string email)
        {
            Diacono diacono = new Diacono();

            using (var db = new PortalJDContexto())
            {
                diacono = (from d in db.Diacono
                           where d.Email == email
                           select d).FirstOrDefault();
            }

            return diacono;
        }

        public List<Diacono> ListarDiaconos()
        {
            List<Diacono> lstDiaconos = new List<Diacono>();

            using (var db = new PortalJDContexto())
            {
                lstDiaconos = (from d in db.Diacono
                               select d).ToList();
            }

            return lstDiaconos;
        }

        public Diacono RecuperarDiacono(int idDiacono)
        {
            Diacono diacono = new Diacono();

            using (var db = new PortalJDContexto())
            {
                diacono = (from d in db.Diacono
                               where d.ID == idDiacono
                               select d).FirstOrDefault();
            }

            return diacono;
        }
    }
}
