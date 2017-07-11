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
        readonly Contexto _contexto;

        public UsuarioRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Criar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
        }

        public void Alterar(Usuario usuario)
        {
            _contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            _contexto.SaveChanges();
        }
        public void Excluir(Usuario usuario)
        {
            _contexto.Usuarios.Remove(usuario);
            _contexto.SaveChanges();
        }

        public IEnumerable<Usuario> Listar()
        {
            return _contexto.Usuarios.ToList();
        }

        public Usuario Obter(string email)
        {
            return _contexto.Usuarios
                .Where(u => u.Email == email)
                .Include(u => u.Permissoes)
                .FirstOrDefault();
        }

        public Usuario ObterUsuarioLogado(string basicAuth)
        {
            return Obter(Usuario.ObterEmail(basicAuth));
        }

        public Usuario ObterPorId(int id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Id==id);
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
