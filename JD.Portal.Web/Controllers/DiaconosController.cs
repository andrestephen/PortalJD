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
    }
}