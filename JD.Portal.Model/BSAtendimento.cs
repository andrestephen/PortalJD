using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSAtendimento
    {
        public List<Atendimento> ListarAtendimentos()
        {
            List<Atendimento> lstAtendimentos = new List<Atendimento>();

            using (var db = new PortalJDContexto())
            {
                lstAtendimentos = (from a in db.Atendimento.Include("Pessoa").Include("Diacono")
                                   select a).ToList();
            }

            return lstAtendimentos;
        }

        public void AdicionarAtendimento(Atendimento atendimento)
        {
            atendimento.DataAtendimento = DateTime.Now;
            atendimento.DataAtualizacao = atendimento.DataAtendimento;
            atendimento.Status = false;

            //Removendo possíveis máscaras e espaços em branco
            if (atendimento.Pessoa != null)
            {
                if (!string.IsNullOrEmpty(atendimento.Pessoa.CPF))
                    atendimento.Pessoa.CPF = atendimento.Pessoa.CPF.Replace(".", "").Replace("-", "").Trim();

                if (!string.IsNullOrEmpty(atendimento.Pessoa.TelefonePrincipal))
                    atendimento.Pessoa.TelefonePrincipal = atendimento.Pessoa.TelefonePrincipal.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

                if (!string.IsNullOrEmpty(atendimento.Pessoa.TelefoneSecundario))
                    atendimento.Pessoa.TelefoneSecundario = atendimento.Pessoa.TelefoneSecundario.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

                if (!string.IsNullOrEmpty(atendimento.Pessoa.RG))
                    atendimento.Pessoa.RG = atendimento.Pessoa.RG.Trim();

                if (!string.IsNullOrEmpty(atendimento.Pessoa.Nome))
                    atendimento.Pessoa.Nome = atendimento.Pessoa.Nome.Trim();

                if (!string.IsNullOrEmpty(atendimento.Pessoa.Endereco))
                    atendimento.Pessoa.Endereco = atendimento.Pessoa.Endereco.Trim();
            }

            using (var db = new PortalJDContexto())
            {
                db.Atendimento.Add(atendimento);
                db.SaveChanges();
            }
        }

        public Atendimento RecuperarAtendimento(int idAtendimento)
        {
            Atendimento atendimento = new Atendimento();

            using (var db = new PortalJDContexto())
            {
                atendimento = (from a in db.Atendimento.Include("Pessoa").Include("Diacono").Include("AtualizacoesAtendimentos").Include("AtualizacoesAtendimentos.Diacono").Include("Diaconos").Include("Arquivos")
                               where a.ID == idAtendimento
                               select a).FirstOrDefault();
            }

            return atendimento;
        }

        public void AtualizarStatusAtendimento(int idAtendimento, bool status)
        {
            Atendimento atendimento = new Atendimento();

            using (var db = new PortalJDContexto())
            {
                atendimento = db.Atendimento.Where(x => x.ID == idAtendimento).FirstOrDefault();
                atendimento.Status = status;

                db.SaveChanges();
            }
        }

        public void AtualizarInformacaoAtendimento(int idAtendimento, int idDiacono, string descricaoAtualizacao)
        {
            using (var db = new PortalJDContexto())
            {
                DateTime dataAtualizacao = DateTime.Now;

                AtualizacaoAtendimento atualizacaoAtendimento = new AtualizacaoAtendimento();
                atualizacaoAtendimento.DataAtualizacao = dataAtualizacao;
                atualizacaoAtendimento.DescricaoAtualizacao = descricaoAtualizacao;
                atualizacaoAtendimento.DiaconoID = idDiacono;
                atualizacaoAtendimento.AtendimentoID = idAtendimento;

                db.AtualizacaoAtendimento.Add(atualizacaoAtendimento);

                Atendimento atendimento = db.Atendimento.Where(x => x.ID == idAtendimento).First();
                atendimento.DataAtualizacao = dataAtualizacao;

                db.SaveChanges();
            }
        }


        public List<Diacono> ListarDiaconosNoAtendimento(int idAtendimento)
        {
            List<Diacono> lstDiaconos = new List<Diacono>();

            using (var db = new PortalJDContexto())
            {
                lstDiaconos = (from d in db.Diacono
                               from p in d.Atendimentos
                               where p.ID == idAtendimento
                               select d).ToList();

                return lstDiaconos;
            }
        }

        public void RemoverDiaconosAtendimento(int idAtendimento, List<int> idsDiaconos)
        {
            Atendimento atendimento = new Atendimento();

            using (var db = new PortalJDContexto())
            {
                atendimento = db.Atendimento.Where(x => x.ID == idAtendimento).FirstOrDefault();
                foreach (int idDiacono in idsDiaconos)
                {
                    atendimento.Diaconos.RemoveAll(x => x.ID == idDiacono);
                }

                db.SaveChanges();
            }
        }

        public void AdicionarDiaconosAtendimento(int idAtendimento, List<int> idsDiaconos)
        {
            Atendimento atendimento = new Atendimento();

            using (var db = new PortalJDContexto())
            {
                atendimento = db.Atendimento.Where(x => x.ID == idAtendimento).FirstOrDefault();
                foreach (int idDiacono in idsDiaconos)
                {
                    Diacono diacono = db.Diacono.Where(x => x.ID == idDiacono).First();
                    if (diacono != null)
                    {
                        atendimento.Diaconos.Add(diacono);
                    }
                }

                db.SaveChanges();
            }
        }

        public void AdicionarArquivoNoAtendimento(int idAtendimento, Arquivo arquivo)
        {
            Atendimento atendimento = new Atendimento();

            using (var db = new PortalJDContexto())
            {
                atendimento = db.Atendimento.Where(x => x.ID == idAtendimento).FirstOrDefault();
                atendimento.Arquivos.Add(arquivo);

                db.SaveChanges();
            }
        }

        public void ExcluirArquivo(int idArquivo)
        {
            using (var db = new PortalJDContexto())
            {
                Arquivo arquivo = db.Arquivo.Where(x => x.ID == idArquivo).FirstOrDefault();
                db.Arquivo.Remove(arquivo);

                db.SaveChanges();
            }
        }

        public List<Arquivo> ListarArquivosNoAtendimento(int idAtendimento)
        {
            List<Arquivo> lstArquivos = new List<Arquivo>();

            using (var db = new PortalJDContexto())
            {
                Atendimento atendimento = (from p in db.Atendimento.Include("Arquivos")
                                           where p.ID == idAtendimento
                                           select p).FirstOrDefault();

                lstArquivos = atendimento.Arquivos;

                return lstArquivos;
            }
        }
    }
}
