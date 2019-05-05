using JD.Portal.Web.Util;
using System.Web.Mvc;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}
