using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
    public class ReunioesController : Controller
    {
        // GET: Reunioes
        public ActionResult Index()
        {
            return View();
        }
    }
}