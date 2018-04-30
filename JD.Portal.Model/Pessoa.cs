using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Pessoas")]
    public class Pessoa
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome da pessoa é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }
        public string RG { get; set; }
        [MaxLength(11)]
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
    }
}
