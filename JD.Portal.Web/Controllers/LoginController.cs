using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string senha)
        {
            try
            {
                BSDiacono bsDiacono = new BSDiacono();
                if (bsDiacono.AutenticarUsuario(email, senha))
                {
                    Diacono diacono = bsDiacono.RecuperarDiaconoPorEmail(email);

                    Session["UsuarioLogado"] = diacono;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["mensagem"] = "E-mail ou senha incorretos";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [ValidacaoLogado]
        public  ActionResult Sair()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}