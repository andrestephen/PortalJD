using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JD.Portal.Model;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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

                return RedirectToAction("Acompanhamento", "Projetos", new { @id = projeto.ID });
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
        public ActionResult AtualizarInformacaoProjeto(int idProjeto, int idDiacono, string descricaoAtualizacao)
        {
            BSProjeto bsProjeto = new BSProjeto();

            bsProjeto.AtualizarInformacaoProjeto(idProjeto, idDiacono, descricaoAtualizacao);

            Projeto projeto = bsProjeto.RecuperarProjeto(idProjeto);

            var listaAtualizacoes = from a in projeto.AtualizacoesProjetos
                                    select new
                                    {
                                        nomeDiacono = a.Diacono.Nome,
                                        dataAtualizacao = a.DataAtualizacao.ToString("dd/MM/yyyy HH:mm"),
                                        descricaoAtualizacao = a.DescricaoAtualizacao
                                    };


            return Json(new { listaAtualizacoes });
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


        public ActionResult ListarTodosDiaconos(int idProjeto)
        {
            BSProjeto bsProjeto = new BSProjeto();
            BSDiacono bsDiacono = new BSDiacono();

            List<Diacono> lstDiaconosNoProjeto = bsProjeto.ListarDiaconosNoProjeto(idProjeto);
            List<Diacono> lstTodosDiaconos = bsDiacono.ListarDiaconos();

            var listaDiaconos = from d in lstTodosDiaconos
                                select new
                                {
                                    id = d.ID,
                                    nomeDiacono = d.Nome,
                                    responsavel = lstDiaconosNoProjeto.Where(x => x.ID == d.ID).Count() > 0
                                };


            return Json(new { listaDiaconos = listaDiaconos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AtualizarResponsaveisProjeto(int idProjeto, List<Models.vmDiaconoResponsavel> listaDiaconosResponsaveis)
        {
            BSProjeto bsProjeto = new BSProjeto();
            List<int> idsDiaconosRemover = (from d in listaDiaconosResponsaveis
                                           where d.responsavel == false
                                           select d.id).ToList();

            List<int> idDiaconosAdicionar = (from d in listaDiaconosResponsaveis
                                             where d.responsavel == true
                                             select d.id).ToList();

            bsProjeto.RemoverDiaconosProjeto(idProjeto, idsDiaconosRemover);
            bsProjeto.AdicionarDiaconosProjeto(idProjeto, idDiaconosAdicionar);            

            return Json(new { listaDiaconosResponsaveis = listaDiaconosResponsaveis.Where(x => x.responsavel == true).ToList() });
        }

        [HttpPost]
        public ContentResult Upload()
        {
            //string path = Server.MapPath("~/Uploads/");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                //postedFile.SaveAs(path + postedFile.FileName);

                CloudBlobContainer container = this.GetCloudBlobContainer();
               
                bool sucesso  = container.CreateIfNotExists();
                string nomeContainer = container.Name;

                CloudBlockBlob blob = container.GetBlockBlobReference("myBlob");
                using (var fileStream = postedFile.InputStream)
                {
                    blob.UploadFromStream(fileStream);
                }
               
            }

            return Content("Success");
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("portaljdstorage"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("portaljd-blob-container");
            return container;
        }
    }
}