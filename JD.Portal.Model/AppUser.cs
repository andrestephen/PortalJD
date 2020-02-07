using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class AppUser : IdentityUser
    {
        //add your custom properties which have not included in IdentityUser before

        public int DiaconoID { get; set; }
        [ForeignKey("DiaconoID")]
        public virtual Diacono Diacono { get; set; }

    }
}
