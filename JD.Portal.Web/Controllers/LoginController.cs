using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;
using JD.Portal.Web.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

                //if (ModelState.IsValid)
                //{
                //    var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                //    var authManager = HttpContext.GetOwinContext().Authentication;

                //    AppUser user = userManager.Find(login.UserName, login.Password);
                //    if (user != null)
                //    {
                //        var ident = userManager.CreateIdentity(user,
                //            DefaultAuthenticationTypes.ApplicationCookie);
                //        AuthManager.SignIn(
                //            new AuthenticationProperties { IsPersistent = false }, ident);
                //        return Redirect(login.ReturnUrl ?? Url.Action("Index", "Home"));
                //    }
                //}
                //ModelState.AddModelError("", "Invalid username or password");
                //return View(login);

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