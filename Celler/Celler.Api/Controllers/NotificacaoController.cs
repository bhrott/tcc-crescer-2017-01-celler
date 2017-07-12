using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;

namespace Celler.Api.Controllers
{
    public class NotificacaoController : ControllerBasica
    {
        readonly NotificacaoRepositorio _notificacaoRepositorio;
        readonly Contexto _contexto = new Contexto();

        public NotificacaoController()
        {
            _notificacaoRepositorio = new NotificacaoRepositorio(_contexto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _notificacaoRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}