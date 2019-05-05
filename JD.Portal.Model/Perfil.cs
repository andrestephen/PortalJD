using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Perfis")]
    public class Perfil
    {
        public int ID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "O nome do perfil é obrigatório.")]
        public string Nome { get; set; }

        public virtual List<Diacono> Diaconos { get; set; }
    }
}
