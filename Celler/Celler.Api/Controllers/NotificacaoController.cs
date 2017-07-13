using Celler.Api.App_Start;
using Celler.Dominio.Entidades;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("notificacao")]
    public class NotificacaoController : ControllerBasica
    {
        readonly NotificacaoRepositorio _notificacaoRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly Contexto _contexto = new Contexto();

        public NotificacaoController()
        {
            _notificacaoRepositorio = new NotificacaoRepositorio(_contexto);
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
        }

        [HttpGet]
        public HttpResponseMessage ObterNotificacoes()
        {
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var notificacoes = _notificacaoRepositorio.ObterNotificacoes(usuarioLogado);
            return ResponderOk(notificacoes);
        }

        [HttpPut]
        public HttpResponseMessage LerNotificacao(int id)
        {
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var notificacao = _notificacaoRepositorio.Obter(id);
            notificacao.Ler();
            _notificacaoRepositorio.Alterar(notificacao);
            return ResponderOk();
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