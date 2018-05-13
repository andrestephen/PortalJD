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
                atendimento = (from a in db.Atendimento.Include("Pessoa").Include("Diacono").Include("AtualizacoesAtendimentos").Include("AtualizacoesAtendimentos.Diacono")
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
    }
}
