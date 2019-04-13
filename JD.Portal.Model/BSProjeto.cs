using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSProjeto
    {
        public enum StatusProjeto
        {
            novo = 1,
            aprovado = 2,
            nao_aprovado = 3,
            concluido = 4
        }

        public List<Projeto> ListarProjetos()
        {
            List<Projeto> lstProjetos = new List<Projeto>();

            using (var db = new PortalJDContexto())
            {
                lstProjetos = (from a in db.Projeto.Include("BeneficiarioProjeto").Include("Diacono")
                               select a).ToList();
            }

            return lstProjetos;
        }

        public void AdicionarProjeto(Projeto projeto)
        {
            projeto.DataCriacao = DateTime.Now;
            projeto.DataAtualizacao = projeto.DataCriacao;
            projeto.Status = (int)StatusProjeto.novo;

            //Removendo possíveis máscaras e espaços em branco
            if (projeto.BeneficiarioProjeto != null)
            {
                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.TelefonePrincipal))
                    projeto.BeneficiarioProjeto.TelefonePrincipal = projeto.BeneficiarioProjeto.TelefonePrincipal.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.TelefoneSecundario))
                    projeto.BeneficiarioProjeto.TelefoneSecundario = projeto.BeneficiarioProjeto.TelefoneSecundario.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.Nome))
                    projeto.BeneficiarioProjeto.Nome = projeto.BeneficiarioProjeto.Nome.Trim();

                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.NomeContato))
                    projeto.BeneficiarioProjeto.NomeContato = projeto.BeneficiarioProjeto.NomeContato.Trim();

                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.EmailContato))
                    projeto.BeneficiarioProjeto.EmailContato = projeto.BeneficiarioProjeto.EmailContato.Trim();

                if (!string.IsNullOrEmpty(projeto.BeneficiarioProjeto.Endereco))
                    projeto.BeneficiarioProjeto.Endereco = projeto.BeneficiarioProjeto.Endereco.Trim();
            }

            using (var db = new PortalJDContexto())
            {
                db.Projeto.Add(projeto);
                db.SaveChanges();
            }
        }

        public Projeto RecuperarProjeto(int idProjeto)
        {
            Projeto projeto = new Projeto();

            using (var db = new PortalJDContexto())
            {
                projeto = (from a in db.Projeto.Include("BeneficiarioProjeto").Include("Diacono").Include("AtualizacoesProjetos").Include("AtualizacoesProjetos.Diacono").Include("Diaconos").Include("Arquivos")
                           where a.ID == idProjeto
                           select a).FirstOrDefault();
            }

            return projeto;
        }

        public void AtualizarInformacaoProjeto(int idProjeto, int idDiacono, string descricaoAtualizacao)
        {
            using (var db = new PortalJDContexto())
            {
                DateTime dataAtualizacao = DateTime.Now;

                AtualizacaoProjeto atualizacaoProjeto = new AtualizacaoProjeto();
                atualizacaoProjeto.DataAtualizacao = dataAtualizacao;
                atualizacaoProjeto.DescricaoAtualizacao = descricaoAtualizacao;
                atualizacaoProjeto.DiaconoID = idDiacono;
                atualizacaoProjeto.ProjetoID = idProjeto;

                db.AtualizacaoProjeto.Add(atualizacaoProjeto);

                Atendimento atendimento = db.Atendimento.Where(x => x.ID == idProjeto).First();
                atendimento.DataAtualizacao = dataAtualizacao;

                db.SaveChanges();
            }
        }

        public void AtualizarStatusProjeto(int idProjeto, int statusProjeto)
        {
            Projeto projeto = new Projeto();

            using (var db = new PortalJDContexto())
            {
                projeto = db.Projeto.Where(x => x.ID == idProjeto).FirstOrDefault();
                projeto.Status = statusProjeto;

                db.SaveChanges();
            }
        }

        public List<Diacono> ListarDiaconosNoProjeto(int idProjeto)
        {
            List<Diacono> lstDiaconos = new List<Diacono>();

            using (var db = new PortalJDContexto())
            {
                lstDiaconos = (from d in db.Diacono
                               from p in d.Projetos
                               where p.ID == idProjeto
                               select d).ToList();

                return lstDiaconos;
            }
        }

        public void RemoverDiaconosProjeto(int idProjeto, List<int> idsDiaconos)
        {
            Projeto projeto = new Projeto();

            using (var db = new PortalJDContexto())
            {
                projeto = db.Projeto.Where(x => x.ID == idProjeto).FirstOrDefault();
                foreach (int idDiacono in idsDiaconos)
                {
                    projeto.Diaconos.RemoveAll(x => x.ID == idDiacono);
                }

                db.SaveChanges();
            }
        }

        public void AdicionarDiaconosProjeto(int idProjeto, List<int> idsDiaconos)
        {
            Projeto projeto = new Projeto();

            using (var db = new PortalJDContexto())
            {
                projeto = db.Projeto.Where(x => x.ID == idProjeto).FirstOrDefault();
                foreach (int idDiacono in idsDiaconos)
                {
                    Diacono diacono = db.Diacono.Where(x => x.ID == idDiacono).First();
                    if (diacono != null)
                    {
                        projeto.Diaconos.Add(diacono);
                    }
                }

                db.SaveChanges();
            }
        }

        public void AdicionarArquivoNoProjeto(int idProjeto, Arquivo arquivo)
        {
            Projeto projeto = new Projeto();

            using (var db = new PortalJDContexto())
            {
                projeto = db.Projeto.Where(x => x.ID == idProjeto).FirstOrDefault();
                projeto.Arquivos.Add(arquivo);

                db.SaveChanges();
            }
        }

        public List<Arquivo> ListarArquivosNoProjeto(int idProjeto)
        {
            List<Arquivo> lstArquivos = new List<Arquivo>();

            using (var db = new PortalJDContexto())
            {
                Projeto projeto = (from p in db.Projeto.Include("Arquivos")
                                   where p.ID == idProjeto
                                   select p).FirstOrDefault();

                lstArquivos = projeto.Arquivos;

                return lstArquivos;
            }
        }


    }
}
