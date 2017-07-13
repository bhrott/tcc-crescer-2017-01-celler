using Celler.Dominio.Entidades;
using System.Linq;
using System.Data.Entity;
using System;

namespace Celler.Infraestrutura.Repositorios
{
    public class NotificacaoRepositorio
    {
        readonly Contexto _contexto;

        public NotificacaoRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public Notificacao Obter(int id)
        {
            return _contexto.Notificacao
                .Include(x => x.Usuario)
                .FirstOrDefault(x => x.Id == id);
        }

        public dynamic ObterNotificacoes(Usuario usuario)
        {
            return _contexto.Notificacao
                            .Include(x => x.Usuario)
                            .Where(x => x.Usuario.Id == usuario.Id)
                            .Select(x => new
                            {
                                id = x.Id,
                                texto = x.Texto,
                                link = x.Link,
                                status = x.Status
                            });
        }

        public void CriarNotificacao(Notificacao notificacao)
        {
            _contexto.Notificacao.Add(notificacao);
            _contexto.SaveChanges();
        }

        public void Alterar(Notificacao notificacao)
        {
            _contexto.Entry(notificacao).State = System.Data.Entity.EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
