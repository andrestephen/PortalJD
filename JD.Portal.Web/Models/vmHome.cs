using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JD.Portal.Web.Models
{
    public class vmHome
    {
        public List<Model.Diacono> DiaconosDiretoria { get; set; }
        public List<Model.Projeto> Projetos { get; set; }
        public List<Model.Atendimento> Atendimentos { get; set; }
        public List<Model.Visita> Visitas { get; set; }
    }
}