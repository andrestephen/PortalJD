using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JD.Portal.Model
{
    public class BSDiacono
    {
        public enum PerfilDiacono
        {
            diacono = 1,
            presidente = 2,
            vicepresidente = 3,
            tesoureiro = 4,
            primeirosecretario = 5,
            segundosecretario = 6,
            pastor = 7,
            administrador = 8
        }

        public bool AutenticarUsuario(string email, string senha)
        {
            bool autenticado = false;

            senha = this.GerarHash(senha);

            using (var db = new PortalJDContexto())
            {
                autenticado = (from d in db.Diacono
                               where d.Email == email && d.Senha == senha && d.Ativo == true
                               select d).Count() > 0;
            }

            return autenticado;
        }

        public Diacono RecuperarDiaconoPorEmail(string email)
        {
            Diacono diacono = new Diacono();

            using (var db = new PortalJDContexto())
            {
                diacono = (from d in db.Diacono
                           where d.Email == email
                           select d).FirstOrDefault();
            }

            return diacono;
        }

        public List<Diacono> ListarDiaconos()
        {
            List<Diacono> lstDiaconos = new List<Diacono>();

            using (var db = new PortalJDContexto())
            {
                lstDiaconos = (from d in db.Diacono
                               select d).ToList();
            }

            return lstDiaconos;
        }

        public Diacono RecuperarDiacono(int idDiacono)
        {
            Diacono diacono = new Diacono();

            using (var db = new PortalJDContexto())
            {
                diacono = (from d in db.Diacono.Include("Perfis")
                           where d.ID == idDiacono
                           select d).FirstOrDefault();
            }

            return diacono;
        }

        public void AdicionarDiacono(Diacono diacono)
        {
            diacono.DataCadastro = DateTime.Now;
            //diacono.DataAtualizacao = diacono.DataCriacao;
            diacono.Ativo = true;

            //Removendo possíveis máscaras e espaços em branco
            if (!string.IsNullOrEmpty(diacono.Telefone))
                diacono.Telefone = diacono.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

            if (!string.IsNullOrEmpty(diacono.Nome))
                diacono.Nome = diacono.Nome.Trim();

            if (!string.IsNullOrEmpty(diacono.Email))
                diacono.Email = diacono.Email.Trim();

            //Gerar o hash simples da senha
            diacono.Senha = this.GerarHash(diacono.Senha);

            using (var db = new PortalJDContexto())
            {
                if (db.Diacono.Where(x => x.Email == diacono.Email).Count() > 0)
                    throw new Exception("O e-mail informado já está cadastrado.");

                List<Perfil> perfis = new List<Perfil>();
                foreach (Perfil perfil in diacono.Perfis)
                {
                    perfis.Add(db.Perfil.Where(x => x.ID == perfil.ID).First());
                }

                diacono.Perfis = perfis;
                db.Diacono.Add(diacono);
                db.SaveChanges();
            }
        }

        public void EditarDiacono(Diacono diacono)
        {
            //Removendo possíveis máscaras e espaços em branco
            if (!string.IsNullOrEmpty(diacono.Telefone))
                diacono.Telefone = diacono.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

            if (!string.IsNullOrEmpty(diacono.Nome))
                diacono.Nome = diacono.Nome.Trim();

            if (!string.IsNullOrEmpty(diacono.Email))
                diacono.Email = diacono.Email.Trim();

            using (var db = new PortalJDContexto())
            {
                Diacono diaconoExistente = db.Diacono.Where(x => x.ID == diacono.ID).First();

                //fazer um foreach nos perfis e excluir e adicionar

                diaconoExistente.Nome = diacono.Nome;
                diaconoExistente.Ativo = diacono.Ativo;
                diaconoExistente.Email = diacono.Email;
                diaconoExistente.Foto = diacono.Foto;
                diaconoExistente.Telefone = diacono.Telefone;
                //Gerar o hash simples da senha
                if (!string.IsNullOrEmpty(diacono.Senha))
                {
                    diaconoExistente.Senha = this.GerarHash(diacono.Senha);
                }

                //Remover os perfis
                List<int> idsPerfisRemover = new List<int>();
                foreach (Perfil perfil in diaconoExistente.Perfis)
                {
                    if(diacono.Perfis.Where(x => x.ID == perfil.ID).Count() == 0)
                    {
                        idsPerfisRemover.Add(perfil.ID);
                    }
                }

                foreach (int idPerfilRemover in idsPerfisRemover)
                {
                    Perfil perfilRemover = diaconoExistente.Perfis.Where(x => x.ID == idPerfilRemover).FirstOrDefault();
                    diaconoExistente.Perfis.Remove(perfilRemover);
                }

                foreach (Perfil perfil in diacono.Perfis)
                {
                    diaconoExistente.Perfis.Add(db.Perfil.Where(x => x.ID == perfil.ID).First());
                }

                db.SaveChanges();
            }
        }

        private string GerarHash(string senha)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(senha);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }


    }
}
