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
            return View();
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

                TempData["cadastroNovoAtendimentoSucesso"] = true;

                return RedirectToAction("Index", "Atendimentos");
            }
            catch (Exception)
            {
                return View(atendimento);
            }
        }

    }
}