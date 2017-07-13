using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    public class NotificacaoController : ControllerBasica
    {
        readonly NotificacaoRepositorio _notificacaoRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly Contexto _contexto = new Contexto();

        public NotificacaoController()
        {
            _notificacaoRepositorio = new NotificacaoRepositorio(_contexto);
        }

        [HttpGet, Route("notificacoes")]
        public HttpResponseMessage ObterNotificacoes()
        {
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var notificacoes = _notificacaoRepositorio.ObterNotificacoes(usuarioLogado);
            return ResponderOk(notificacoes);
        }

        [HttpGet, Route("notificacoes")]
        public HttpResponseMessage ObterNotificacoes()
        {
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var notificacoes = _notificacaoRepositorio.ObterNotificacoes(usuarioLogado);
            return ResponderOk(notificacoes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _notificacaoRepositorio.Dispose();
                _usuarioRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}