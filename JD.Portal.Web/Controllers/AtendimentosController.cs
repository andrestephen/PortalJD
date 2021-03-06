﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;
using JD.Portal.Web.Util;

namespace JD.Portal.Web.Controllers
{
    [ValidacaoLogado]
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
                atendimento.DiaconoID = ((Diacono)Session["UsuarioLogado"]).ID;
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

        [HttpPost]
        public ActionResult Upload(int idAtendimento)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();

            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];

                Util.Storage storage = new Util.Storage();

                Arquivo arquivo = new Arquivo();

                arquivo.TamanhoBytes = postedFile.ContentLength;
                arquivo.Tipo = postedFile.ContentType;
                arquivo.DataCriacao = DateTime.Now;
                arquivo.Nome = storage.SalvarBlob(postedFile);

                bsAtendimento.AdicionarArquivoNoAtendimento(idAtendimento, arquivo);
            }

            List<Arquivo> lstArquivos = bsAtendimento.ListarArquivosNoAtendimento(idAtendimento);

            var listaRetorno = from a in lstArquivos
                               select new
                               {
                                   id = a.ID,
                                   nome = a.Nome.Substring(a.Nome.IndexOf('_') + 1),
                                   tamanho = a.TamanhoBytes.ToString(),
                                   tipo = a.Tipo,
                                   url = "https://portaljd.blob.core.windows.net/portaljd-blob-container/" + a.Nome,
                                   dataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm")
                               };


            return Json(new { listaArquivos = listaRetorno });//listaDiaconosResponsaveis.Where(x => x.responsavel == true).ToList() });
        }

        public ActionResult ExcluirArquivo(int idArquivo, int idAtendimento)
        {
            BSAtendimento bsAtendimento = new BSAtendimento();

            bsAtendimento.ExcluirArquivo(idArquivo);

            List<Arquivo> lstArquivos = bsAtendimento.ListarArquivosNoAtendimento(idAtendimento);

            var listaRetorno = from a in lstArquivos
                               select new
                               {
                                   id = a.ID,
                                   nome = a.Nome.Substring(a.Nome.IndexOf('_') + 1),
                                   tamanho = a.TamanhoBytes.ToString(),
                                   tipo = a.Tipo,
                                   url = "https://portaljd.blob.core.windows.net/portaljd-blob-container/" + a.Nome,
                                   dataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm")
                               };


            return Json(new { listaArquivos = listaRetorno });
        }
    }
}