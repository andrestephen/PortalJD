using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
    public class TesourariaController : Controller
    {
        // GET: Tesouraria
        public ActionResult Index()
        {
            return View();
        }
    }
}