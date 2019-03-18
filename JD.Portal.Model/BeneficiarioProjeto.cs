using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("BeneficiariosProjetos")]
    public class BeneficiarioProjeto
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome da beneficiário do projeto é obrigatório")]
        [MaxLength(250)]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        [MaxLength(100)]
        public string NomeContato { get; set; }
        [MaxLength(100)]
        public string EmailContato { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
    }
}
