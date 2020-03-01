using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JD.Portal.Model
{
    [Table("Visitas")]
    public class Visita
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "A data da visita é obrigatória.")]
        public DateTime DataVisita { get; set; }
        [Required(ErrorMessage = "O local da visita é obrigatório.")]
        public string LocalVisita { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "O nome do visitado obrigatório.")]
        public string NomeVisitado { get; set; }
        [Required(ErrorMessage = "O resumo da visita é obrigatória.")]
        public string Resumo { get; set; }

        public int DiaconoID { get; set; }
        [ForeignKey("DiaconoID")]
        public virtual Diacono Diacono { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public virtual List<Diacono> Diaconos { get; set; }
    }
}
