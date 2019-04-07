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
            BSAtendimento bsAtendimento = new BSAtendimento();
            List<Model.Atendimento> atendimentos = bsAtendimento.ListarAtendimentos();

            ViewBag.atendimentosAbertos = atendimentos.Count(x => x.Status == false);
            ViewBag.atendimentosArquivados = atendimentos.Count(x => x.Status == true);

            return View(atendimentos);
        }

        public ActionResult NovoAtendimento()
        {
            return View(new Atendimento());
        }

        [HttpPost]
        public ActionResult NovoAtendimento(Model.Atendimento atendimento)
        {
            try
            {
                BSAtendimento bsAtendimento = new BSAtendimento();
                bsAtendimento.AdicionarAtendimento(atendimento);

                if (atendimento.ID > 0)
                {
                    TempData["cadastroNovoAtendimentoSucesso"] = true;
                    TempData["idRecemAdicionado"] = atendimento.ID;
                }

                return RedirectToAction("Acompanhamento", "Atendimentos", new { @id = atendimento.ID });
            }
            catch (Exception)
            {
                return View(atendimento);
            }
        }

        public ActionResult Acompanhamento(int id)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();
            Atendimento atendimento = bsAtendimento.RecuperarAtendimento(id);
            return View(atendimento);
        }

        [HttpPost]
        public ActionResult AtualizarInformacaoAtendimento(int idAtendimento, int idDiacono, string descricaoAtualizacao)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();

            bsAtendimento.AtualizarInformacaoAtendimento(idAtendimento, idDiacono, descricaoAtualizacao);

            Atendimento atendimento = bsAtendimento.RecuperarAtendimento(idAtendimento);

            var listaAtualizacoes = from a in atendimento.AtualizacoesAtendimentos
                                    select new
                                    {
                                        nomeDiacono = a.Diacono.Nome,
                                        dataAtualizacao = a.DataAtualizacao.ToString("dd/MM/yyyy HH:mm"),
                                        descricaoAtualizacao = a.DescricaoAtualizacao
                                    };


            return Json(new { listaAtualizacoes });
        }

        [HttpPost]
        public ActionResult AtualizarStatusAtendimento(int idAtendimento, bool statusAtendimento)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();

            bsAtendimento.AtualizarStatusAtendimento(idAtendimento, statusAtendimento);

            Atendimento atendimento = bsAtendimento.RecuperarAtendimento(idAtendimento);
            return Json(new { statusAtendimento = statusAtendimento });
        }

        public ActionResult ListarTodosDiaconos(int idAtendimento)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();
            BSDiacono bsDiacono = new BSDiacono();

            List<Diacono> lstDiaconosNoAtendimento = bsAtendimento.ListarDiaconosNoAtendimento(idAtendimento);
            List<Diacono> lstTodosDiaconos = bsDiacono.ListarDiaconos();

            var listaDiaconos = from d in lstTodosDiaconos
                                select new
                                {
                                    id = d.ID,
                                    nomeDiacono = d.Nome,
                                    responsavel = lstDiaconosNoAtendimento.Where(x => x.ID == d.ID).Count() > 0
                                };


            return Json(new { listaDiaconos = listaDiaconos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AtualizarResponsaveisAtendimento(int idAtendimento, List<Models.vmDiaconoResponsavel> listaDiaconosResponsaveis)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();
            List<int> idsDiaconosRemover = (from d in listaDiaconosResponsaveis
                                            where d.responsavel == false
                                            select d.id).ToList();

            List<int> idDiaconosAdicionar = (from d in listaDiaconosResponsaveis
                                             where d.responsavel == true
                                             select d.id).ToList();

            bsAtendimento.RemoverDiaconosAtendimento(idAtendimento, idsDiaconosRemover);
            bsAtendimento.AdicionarDiaconosAtendimento(idAtendimento, idDiaconosAdicionar);

            return Json(new { listaDiaconosResponsaveis = listaDiaconosResponsaveis.Where(x => x.responsavel == true).ToList() });
        }

    }
}