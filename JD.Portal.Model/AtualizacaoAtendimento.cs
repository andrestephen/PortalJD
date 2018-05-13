using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("AtualizacoesAtendimentos")]
    public class AtualizacaoAtendimento
    {
        public int ID { get; set; }
        public DateTime DataAtualizacao { get; set; }
        [Required(ErrorMessage = "A descrição da atualização é obrigatória.")]
        public string DescricaoAtualizacao { get; set; }

        public int AtendimentoID { get; set; }
        [ForeignKey("AtendimentoID")]
        public virtual Atendimento Atendimento { get; set; }

        public int DiaconoID { get; set; }
        [ForeignKey("DiaconoID")]
        public virtual Diacono Diacono { get; set; }
    }
}
