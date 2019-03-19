using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;

namespace JD.Portal.Web.Controllers
{
    public class ProjetosController : Controller
    {
        // GET: Projetos
        public ActionResult Index()
        {
            BSProjeto bsProjeto = new BSProjeto();
            List<Model.Projeto> projetos = bsProjeto.ListarProjetos();

            ViewBag.projetosNovos = projetos.Count(x => x.Status == (int)BSProjeto.StatusProjeto.novo);
            ViewBag.projetosEmAprovacao = projetos.Count(x => x.Status == (int)BSProjeto.StatusProjeto.em_aprovação);
            ViewBag.projetosAprovados = projetos.Count(x => x.Status == (int)BSProjeto.StatusProjeto.aprovado);
            ViewBag.projetosNaoAprovados = projetos.Count(x => x.Status == (int)BSProjeto.StatusProjeto.nao_aprovado);
            ViewBag.projetosConcluidos = projetos.Count(x => x.Status == (int)BSProjeto.StatusProjeto.concluido);

            return View(projetos);
        }

        public ActionResult NovoProjeto()
        {
            return View(new Projeto());
        }

        [HttpPost]
        public ActionResult NovoProjeto(Model.Projeto projeto)
        {
            try
            {
                BSProjeto bsProjeto = new BSProjeto();
                bsProjeto.AdicionarProjeto(projeto);

                if (projeto.ID > 0)
                {
                    TempData["cadastroNovoProjetoSucesso"] = true;
                    TempData["idRecemAdicionado"] = projeto.ID;
                }

                return RedirectToAction("Index", "Projetos");
            }
            catch (Exception ex)
            {
                return View(projeto);
            }
        }

        public ActionResult Acompanhamento(int id)
        {
            BSProjeto bsProjeto = new BSProjeto();
            Projeto projeto = bsProjeto.RecuperarProjeto(id);
            return View(projeto);
        }
    }
}