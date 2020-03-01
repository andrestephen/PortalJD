using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSVisita
    {
        public List<Visita> ListarVisitas()
        {
            List<Visita> lstVisitas = new List<Visita>();

            using (var db = new PortalJDContexto())
            {
                lstVisitas = (from a in db.Visita
                              select a).ToList();
            }

            return lstVisitas;
        }

        public Visita RecuperarVisita(int idVisita)
        {
            Visita visita = new Visita();

            using (var db = new PortalJDContexto())
            {
                visita = (from v in db.Visita.Include("Diaconos")
                           where v.ID == idVisita
                           select v).FirstOrDefault();
            }

            return visita;
        }

        public void AdicionarVisita(Visita visita)
        {
            visita.DataAtualizacao = DateTime.Now;

            if (!string.IsNullOrEmpty(visita.NomeVisitado))
                visita.NomeVisitado = visita.NomeVisitado.Trim();

            if (!string.IsNullOrEmpty(visita.LocalVisita))
                visita.LocalVisita = visita.LocalVisita.Trim();

            if (!string.IsNullOrEmpty(visita.Resumo))
                visita.Resumo = visita.Resumo.Trim();

            using (var db = new PortalJDContexto())
            {
                List<Diacono> diaconos = new List<Diacono>();
                foreach (Diacono diacono in visita.Diaconos)
                {
                    diaconos.Add(db.Diacono.Where(x => x.ID == diacono.ID).First());
                }

                visita.Diaconos = diaconos;
                db.Visita.Add(visita);
                db.SaveChanges();
            }
        }

        public void EditarVisita(Visita visita)
        {
            //Removendo possíveis máscaras e espaços em branco
            if (!string.IsNullOrEmpty(visita.NomeVisitado))
                visita.NomeVisitado = visita.NomeVisitado.Trim();

            if (!string.IsNullOrEmpty(visita.LocalVisita))
                visita.LocalVisita = visita.LocalVisita.Trim();

            if (!string.IsNullOrEmpty(visita.Resumo))
                visita.Resumo = visita.Resumo.Trim();

            using (var db = new PortalJDContexto())
            {
                Visita visitaExistente = db.Visita.Where(x => x.ID == visita.ID).First();

                //fazer um foreach nos perfis e excluir e adicionar

                visitaExistente.NomeVisitado = visita.NomeVisitado;
                visitaExistente.LocalVisita = visita.LocalVisita;
                visitaExistente.Resumo = visita.Resumo;
                visitaExistente.DataVisita = visita.DataVisita;
                visitaExistente.DataAtualizacao = DateTime.Now;

                //Remover os perfis
                List<int> idsDiaconosRemover = new List<int>();
                foreach (Diacono diacono in visitaExistente.Diaconos)
                {
                    if (visita.Diaconos.Where(x => x.ID == diacono.ID).Count() == 0)
                    {
                        idsDiaconosRemover.Add(diacono.ID);
                    }
                }

                foreach (int idDiaconoRemover in idsDiaconosRemover)
                {
                    Diacono diaconoRemover = visitaExistente.Diaconos.Where(x => x.ID == idDiaconoRemover).FirstOrDefault();
                    visitaExistente.Diaconos.Remove(diaconoRemover);
                }


                foreach (Diacono diacono in visita.Diaconos)
                {
                    visitaExistente.Diaconos.Add(db.Diacono.Where(x => x.ID == diacono.ID).First());
                }

                db.SaveChanges();
            }
        }
    }
}
