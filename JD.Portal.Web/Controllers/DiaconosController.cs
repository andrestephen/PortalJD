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
    public class DiaconosController : Controller
    {
        // GET: Diaconos
        public ActionResult Index()
        {
            BSDiacono bsDiacono = new BSDiacono();
            List<Model.Diacono> diaconos = bsDiacono.ListarDiaconos();

            ViewBag.diaconosAtivos = diaconos.Count(x => x.Ativo == true);
            ViewBag.diaconosInativos = diaconos.Count(x => x.Ativo == false);

            return View(diaconos);
        }

        public ActionResult NovoDiacono()
        {
            return View(new Diacono());
        }

        public ActionResult EditarDiacono(int ID)
        {
            BSDiacono bsDiacono = new BSDiacono();
            Diacono diacono = bsDiacono.RecuperarDiacono(ID);

            return View("NovoDiacono", diacono);
        }

        [HttpPost]
        public ActionResult NovoDiacono(Diacono diacono, string confirmacao, List<int> chkPerfil)
        {
            try
            {
                if (chkPerfil == null || (chkPerfil != null && chkPerfil.Count() == 0))
                {
                    TempData["mensagemAlerta"] = "Selecione um ou mais perfis para o novo diácono.";
                    return View(diacono);
                }

                diacono.Perfis = new List<Perfil>();

                foreach (int perfil in chkPerfil)
                {
                    diacono.Perfis.Add(new Perfil() { ID = perfil });
                }

                BSDiacono bsDiacono = new BSDiacono();

                if (diacono.ID == 0)
                {
                    if (diacono.Senha != confirmacao)
                    {
                        TempData["mensagemAlerta"] = "Senha e confirmação de senha estão diferentes. Digite a senha novamente.";
                        return View(diacono);
                    }

                    bsDiacono.AdicionarDiacono(diacono);

                    if (diacono.ID > 0)
                    {
                        TempData["cadastroNovoDiaconoSucesso"] = true;
                        TempData["idRecemAdicionado"] = diacono.ID;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(diacono.Senha) && diacono.Senha != confirmacao)
                    {
                        TempData["mensagemAlerta"] = "Senha e confirmação de senha estão diferentes. Digite a senha novamente.";
                        return View(diacono);
                    }

                    bsDiacono.EditarDiacono(diacono);

                }
                return RedirectToAction("Index", "Diaconos", new { @id = diacono.ID });
            }
            catch (Exception ex)
            {
                TempData["mensagemErro"] = ex.Message;
                return View(diacono);
            }
        }
    }
}