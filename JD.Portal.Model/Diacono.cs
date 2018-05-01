using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Diaconos")]
    public class Diacono
    {
        public int ID { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "O nome do Diácono é obrigatório.")]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "O e-mail do Diácono é obrigatório.")]
        public string Email { get; set; }
    }
}
