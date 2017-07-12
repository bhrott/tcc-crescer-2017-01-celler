using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System.Net.Http;
using System.Web.Http;

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

        [HttpGet, Route("notificacoes")]
        public HttpResponseMessage ObterNotificacoes()
        {
            return null;
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