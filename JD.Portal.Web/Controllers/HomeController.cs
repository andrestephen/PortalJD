using JD.Portal.Web.Util;
using System.Web.Mvc;
using JD.Portal.Web.Util;
using JD.Portal.Model;
using System.Collections.Generic;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BSProjeto bsProjeto = new BSProjeto();
            List<Projeto> projetos = bsProjeto.ListarProjetos();

            BSAtendimento bsAtendimento = new BSAtendimento();
            List<Atendimento> atendimentos = bsAtendimento.ListarAtendimentos();

            BSDiacono bsDiacono= new BSDiacono();
            List<Diacono> diaconos = bsDiacono.ListarDiretoria();

            BSVisita bsVisita = new BSVisita();
            List<Visita> visitas = bsVisita.ListarVisitas();

            Models.vmHome viewmodelhome = new Models.vmHome();

            viewmodelhome.Atendimentos = atendimentos;
            viewmodelhome.DiaconosDiretoria = diaconos;
            viewmodelhome.Projetos = projetos;
            viewmodelhome.Visitas = visitas;

            return View(viewmodelhome);
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}
