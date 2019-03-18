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
            em_aprovação = 2,
            aprovado = 3,
            nao_aprovado = 4,
            concluido = 5
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
    }
}
