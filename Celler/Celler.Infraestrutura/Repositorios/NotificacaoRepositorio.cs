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

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
