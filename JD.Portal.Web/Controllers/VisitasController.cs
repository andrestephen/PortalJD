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
    public class VisitasController : Controller
    {
        // GET: Diaconos
        public ActionResult Index()
        {
            BSVisita bsVisita = new BSVisita();
            List<Model.Visita> visitas = bsVisita.ListarVisitas();

            return View(visitas);
        }

        public ActionResult NovaVisita()
        {
            List<Model.Diacono> diaconos = new BSDiacono().ListarDiaconosAtivos();
            ViewBag.Diaconos = diaconos;

            return View(new Visita());
        }

        public ActionResult EditarVisita(int ID)
        {
            BSVisita bsVisita = new BSVisita();
            Visita visita = bsVisita.RecuperarVisita(ID);

            List<Model.Diacono> diaconos = new BSDiacono().ListarDiaconosAtivos();
            ViewBag.Diaconos = diaconos;

            return View("NovaVisita", visita);
        }

        [HttpPost]
        public ActionResult NovaVisita(Visita visita, List<int> chkDiaconos)
        {
            try
            {
                if (chkDiaconos == null || (chkDiaconos != null && chkDiaconos.Count() == 0))
                {
                    TempData["mensagemAlerta"] = "Selecione um ou mais diáconos que realizaram a visita.";

                    List<Model.Diacono> diaconos = new BSDiacono().ListarDiaconosAtivos();
                    ViewBag.Diaconos = diaconos;

                    return View(visita);
                }
                visita.DiaconoID = ((Diacono)Session["UsuarioLogado"]).ID;
                visita.Diaconos = new List<Diacono>();

                foreach (int diacono in chkDiaconos)
                {
                    visita.Diaconos.Add(new Diacono() { ID = diacono });
                }

                BSVisita bsVisita = new BSVisita();

                if (visita.ID == 0)
                {
                    bsVisita.AdicionarVisita(visita);

                    if (visita.ID > 0)
                    {
                        TempData["cadastroNovaVisitaSucesso"] = true;
                        TempData["idRecemAdicionado"] = visita.ID;
                    }
                }
                else
                {
                    bsVisita.EditarVisita(visita);
                }
                return RedirectToAction("Index", "Visitas", new { @id = visita.ID });
            }
            catch (Exception ex)
            {
                TempData["mensagemErro"] = ex.Message;

                List<Model.Diacono> diaconos = new BSDiacono().ListarDiaconosAtivos();
                ViewBag.Diaconos = diaconos;

                return View(visita);
            }
        }
    }
}