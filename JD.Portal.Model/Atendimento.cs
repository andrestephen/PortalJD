using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Atendimentos")]
    public class Atendimento
    {
        public int ID { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime DataAtualizacao { get; set; }
        [Required(ErrorMessage = "A descrição do atendimento é obrigatória.")]
        public string Descricao { get; set; }
        public bool Status { get; set; }

        public int PessoaID { get; set; }
        [ForeignKey("PessoaID")]
        public virtual Pessoa Pessoa { get; set; }

        public int DiaconoID { get; set; }
        [ForeignKey("DiaconoID")]
        public virtual Diacono Diacono { get; set; }

    }
}
