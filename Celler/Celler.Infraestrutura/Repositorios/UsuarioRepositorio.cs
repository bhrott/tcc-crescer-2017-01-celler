using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Repositorios
{
    public class UsuarioRepositorio:IDisposable
    {
        private Contexto contexto = new Contexto();



        public UsuarioRepositorio()
        {

        }

        public void Criar(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void Alterar(Usuario usuario)
        {
            contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }
        public void Excluir(Usuario usuario)
        {
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }

        public IEnumerable<Usuario> Listar()
        {
            return contexto.Usuarios.ToList();
        }

        public Usuario Obter(string email)
        {
            return contexto.Usuarios
                .Where(u => u.Email == email)
                .Include(u => u.Permissoes)
                .FirstOrDefault();
        }

        public Usuario ObterUsuarioLogado(string basicAuth)
        {
            return Obter(Usuario.ObterEmail(basicAuth));
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
