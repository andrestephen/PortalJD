using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JD.Portal.Web.Controllers
{
    public class ProjetosController : Controller
    {
        // GET: Projetos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NovoProjeto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoProjeto(string teste)
        {
            return View();
        }
    }
}