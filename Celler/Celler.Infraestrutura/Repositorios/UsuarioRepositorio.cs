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

        public dynamic ObterConfiguracoesNotificacao(string emailUsuario)
        {
            var usuario = Obter(emailUsuario);
            return new
            {
                NotificacaoComentarioAnuncioEmail = usuario.NotificacaoComentarioAnuncioEmail,
                NotificacaoComentarioAnuncioSlack = usuario.NotificacaoComentarioAnuncioSlack,
                NotificacaoComentarioAnuncioBrowser = usuario.NotificacaoComentarioAnuncioBrowser,
                NotificacaoPresencaEmail = usuario.NotificacaoPresencaEmail,
                NotificacaoPresencaSlack = usuario.NotificacaoPresencaSlack,
                NotificacaoPresencaBrowser = usuario.NotificacaoPresencaBrowser,
                NotificacaoInteresseEmail = usuario.NotificacaoInteresseEmail,
                NotificacaoInteresseSlack = usuario.NotificacaoInteresseSlack,
                NotificacaoInteresseBrowser = usuario.NotificacaoInteresseBrowser,
                NotificacaoDoacaoVaquinhaEmail = usuario.NotificacaoDoacaoVaquinhaEmail,
                NotificacaoDoacaoVaquinhaSlack = usuario.NotificacaoDoacaoVaquinhaSlack,
                NotificacaoDoacaoVaquinhaBrowser = usuario.NotificacaoDoacaoVaquinhaBrowser,
                CanalSlack = usuario.CanalSlack
            };
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
