using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Dominio.Entidades;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Entidades;
using Celler.Infraestrutura.Repositorios;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Celler.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/usuario")]

    public class UsuarioController : ControllerBasica
    {
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly Contexto _contexto = new Contexto();

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
        }

        [BasicAuthorization]
        [HttpGet]
        public HttpResponseMessage Obter()
        {
            var usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            if (usuario == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagem = "Usuario/Email não cadastrados." });
            return Request.CreateResponse(HttpStatusCode.OK, new { dados = new { Nome = usuario.Nome, Email = usuario.Email, Id = usuario.Id } });
        }

        [BasicAuthorization]
        [HttpGet, Route("configuracoes")]
        public HttpResponseMessage ObterConfiguracoesNotificacao()
        {
            var reposta = _usuarioRepositorio.ObterConfiguracoesNotificacao(Thread.CurrentPrincipal.Identity.Name);
            return ResponderOk(reposta);
        }

        [BasicAuthorization]
        [HttpPost, Route("configuracoes")]
        public HttpResponseMessage AlterarConfiguracoesNotificacao(ConfiguracoesNotificacaoModel model)
        {
            var usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            usuario.SetarConfiguracoes(model.NotificacaoComentarioAnuncioEmail,
                                       model.NotificacaoComentarioAnuncioSlack,
                                       model.NotificacaoComentarioAnuncioBrowser,
                                       model.NotificacaoPresencaEmail,
                                       model.NotificacaoPresencaSlack,
                                       model.NotificacaoPresencaBrowser,
                                       model.NotificacaoInteresseEmail,
                                       model.NotificacaoInteresseSlack,
                                       model.NotificacaoInteresseBrowser,
                                       model.NotificacaoDoacaoVaquinhaEmail,
                                       model.NotificacaoDoacaoVaquinhaSlack,
                                       model.NotificacaoDoacaoVaquinhaBrowser,
                                       model.CanalSlack);

            if (usuario.Validar())
            {
                _usuarioRepositorio.Alterar(usuario);
                return ResponderOk();
            }
            else
            {
                return ResponderErro(usuario.Mensagens);
            }
        }

        [HttpPost, Route("registrar")]
        public HttpResponseMessage Registrar([FromBody]UsuarioModel model)
        {
            if (_usuarioRepositorio.Obter(model.Email) == null)
            {
                var usuario = new Usuario(model.Nome, model.Email, model.Senha);

                if (usuario.Validar())
                {
                    _usuarioRepositorio.Criar(usuario);
                }
                else
                {
                    return ResponderErro(usuario.Mensagens);
                }
            }
            else
            {
                return ResponderErro("Usuário já existe.");
            }

            return ResponderOk();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _usuarioRepositorio.Dispose();
            base.Dispose(disposing);
        }
    }
}

