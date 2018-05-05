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
            return View();
        }

        [HttpPost]
        public ActionResult NovoAtendimento(Model.Atendimento atendimento)
        {
            return View();
        }

    }
}