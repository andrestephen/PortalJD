using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Projetos")]
    public class Projeto
    {
        public int ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        [Required(ErrorMessage = "O nome do projeto é obrigatório.")]
        [MaxLength(250)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A descrição do projeto é obrigatória.")]
        public string Descricao { get; set; }
        public int Status { get; set; }

        public int BeneficiarioProjetoID { get; set; }
        [ForeignKey("BeneficiarioProjetoID")]
        public virtual BeneficiarioProjeto BeneficiarioProjeto { get; set; }

        public int DiaconoID { get; set; }
        [ForeignKey("DiaconoID")]
        public virtual Diacono Diacono { get; set; }

        public virtual List<AtualizacaoProjeto> AtualizacoesProjetos { get; set; }
    }
}
