using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSAtendimento
    {
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
    }    
}
