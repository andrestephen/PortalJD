using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
    public class DiaconosController : Controller
    {
        // GET: Diaconos
        public ActionResult Index()
        {
            BSDiacono bsDiacono = new BSDiacono();
            List<Model.Diacono> diaconos = bsDiacono.ListarDiaconos();

            ViewBag.diaconosAtivos = diaconos.Count(x => x.Ativo == true);
            ViewBag.diaconosInativos = diaconos.Count(x => x.Ativo == false);

            return View(diaconos);
        }

        public ActionResult NovoDiacono()
        {
            return View(new AppUser());
        }

        [HttpPost]
        public ActionResult NovoProjeto(AppUser appUser)
        {
            //try
            //{
            //    BSDiacono bsProjeto = new BSProjeto();
            //    bsProjeto.AdicionarProjeto(projeto);

            //    if (projeto.ID > 0)
            //    {
            //        TempData["cadastroNovoProjetoSucesso"] = true;
            //        TempData["idRecemAdicionado"] = projeto.ID;
            //    }

            //    return RedirectToAction("Acompanhamento", "Projetos", new { @id = projeto.ID });
            //}
            //catch (Exception ex)
            //{
              return View(new AppUser());
            //}
        }
    }
}