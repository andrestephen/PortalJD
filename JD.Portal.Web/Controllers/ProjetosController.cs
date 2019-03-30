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

        [HttpPost]
        public ActionResult Acompanhamento(int idProjeto, string acaoAcompanhamento, int idDiacono, string descricaoAtualizacao)
        {
            BSProjeto bsProjeto = new BSProjeto();

            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(acaoAcompanhamento))
            {
                switch (acaoAcompanhamento)
                {
                    case "salvarInformacoes":
                        bsProjeto.AtualizarInformacaoProjeto(idProjeto, idDiacono, descricaoAtualizacao);
                        break;
                    //case "arquivar":
                    //    bsAtendimento.AtualizarStatusAtendimento(idAtendimento, true);
                    //    break;
                    //case "reabrir":
                    //    bsAtendimento.AtualizarStatusAtendimento(idAtendimento, false);
                    //    break;
                    default:
                        break;
                }
            }

            Projeto projeto = bsProjeto.RecuperarProjeto(idProjeto);
            return View(projeto);
        }

        //public ActionResult AtualizarStatusProjeto(int idProjeto, int statusProjeto)
        //{
        //    try
        //    {
        //        BSProjeto bsProjeto = new BSProjeto();

        //        bsProjeto.AtualizarStatusProjeto(idProjeto, statusProjeto);

        //        Projeto projeto = bsProjeto.RecuperarProjeto(idProjeto);
        //        return View("Acompanhamento", projeto);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        [HttpPost]
        public ActionResult AtualizarStatusProjeto(int idProjeto, int statusProjeto)
        {
            BSProjeto bsProjeto = new BSProjeto();

            bsProjeto.AtualizarStatusProjeto(idProjeto, statusProjeto);

            Projeto projeto = bsProjeto.RecuperarProjeto(idProjeto);
            return Json(new { statusProjeto = statusProjeto });
        }

        //public ActionResult RetornaProjeto(int id)
        //{
        //    Projeto projeto = new Projeto();
        //    projeto.Descricao = "teste de json";
        //    projeto.ID = 1;
        //    projeto.Nome = "Nome do projeto";

        //    return Json(new { projeto = projeto }, JsonRequestBehavior.AllowGet);
        //}

    }
}