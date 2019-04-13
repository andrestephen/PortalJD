using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Arquivos")]
    public class Arquivo
    {

        public int ID { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required(ErrorMessage = "O nome do arquivo é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Tamanho do arquivo é obrigatório")]
        public int TamanhoBytes { get; set; }
        [Required(ErrorMessage = "O tipo do arquivo é obrigatório")]
        [MaxLength(250)]
        public string Tipo { get; set; }
    }
}
