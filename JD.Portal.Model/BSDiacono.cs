using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSDiacono
    {
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
