using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;

namespace JD.Portal.Web.Controllers
{
    public class AtendimentosController : Controller
    {
        // GET: Atendimentos
        public ActionResult Index()
        {
            BSAtendimento bsAtendimento = new BSAtendimento();
            List<Model.Atendimento> atendimentos = bsAtendimento.ListarAtendimentos();

            ViewBag.atendimentosAbertos = atendimentos.Count(x => x.Status == false);
            ViewBag.atendimentosEncerrados = atendimentos.Count(x => x.Status == true);

            return View(atendimentos);
        }

        public ActionResult NovoAtendimento()
        {
            return View(new Atendimento());
        }

        [HttpPost]
        public ActionResult NovoAtendimento(Model.Atendimento atendimento)
        {
            try
            {
                BSAtendimento bsAtendimento = new BSAtendimento();
                bsAtendimento.AdicionarAtendimento(atendimento);

                if (atendimento.ID > 0)
                {
                    TempData["cadastroNovoAtendimentoSucesso"] = true;
                    TempData["idRecemAdicionado"] = atendimento.ID;
                }

                return RedirectToAction("Index", "Atendimentos");
            }
            catch (Exception)
            {
                return View(atendimento);
            }
        }

        public ActionResult Acompanhamento()
        {
            return View();
        }

    }
}