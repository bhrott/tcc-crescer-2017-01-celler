using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Dominio.Entidades;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using Celler.Infraestrutura.Servicos;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/vaquinha")]

    public class VaquinhaController : ControllerBasica
    {
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly VaquinhaRepositorio _vaquinhaRepositorio;
        readonly Contexto _contexto = new Contexto();

        public VaquinhaController()
        {
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
            _vaquinhaRepositorio = new VaquinhaRepositorio(_contexto);
        }

        [HttpPost, Route("doar")]
        public HttpResponseMessage AdicionarDoadorVaquinha([FromBody] DoarVaquinhaModel model)
        {
            var usuario = _usuarioRepositorio.ObterPorId(model.IdUsuario);
            var vaquinha = _vaquinhaRepositorio.ObterPorId(model.IdVaquinha);

            if (usuario == null || vaquinha == null)
            {
                return ResponderErro("Usuario ou Vaquinha inválidos");
            }

            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            Doador doador = new Doador(usuarioLogado, model.ValorDoado);
            vaquinha.AdicionarDoador(doador);

            if (doador.Validar())
            {
                Notificar notificar = new Notificar(usuario, vaquinha, vaquinha.Criador, new NotificacaoRepositorio(_contexto));
                notificar.NotificarUsuarioDoacaoVaquinha();
                _vaquinhaRepositorio.Alterar(vaquinha);
                _contexto.SaveChanges();
                return ResponderOk(new { texto = "Valor doado com sucesso" });
            }
            else
            {
                return ResponderErro(doador.Mensagens);
            }
        }

        [HttpPost, Route("confirmar")]
        public HttpResponseMessage SalvarVendaProduto([FromBody] ReceberDoacaoModel model)
        {
            var usuario = _usuarioRepositorio.ObterPorId(model.IdUsuario);
            var vaquinha = _vaquinhaRepositorio.ObterPorId(model.IdVaquinha);
            var doador = _vaquinhaRepositorio.ObterDoadorPorId(model.IdDoacao);

            if (usuario == null || vaquinha == null || doador == null)
            {
                return ResponderErro("Usuario ou Vaquinha inválidos.");
            }

            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            if (usuarioLogado != vaquinha.Criador)
            {
                return ResponderErro("Você não tem permissão para aceitar essa doação.");
            }

            double valorDoado = doador.AlterarStatusDoacao(doador);

            if (vaquinha.Validar())
            {
                vaquinha.IncrementarTotal(valorDoado);
                _vaquinhaRepositorio.Alterar(vaquinha);
                _contexto.SaveChanges();
                return ResponderOk(new { texto = "Valor recebido com sucesso" });
            }
            else
            {
                return ResponderErro(vaquinha.Mensagens);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _usuarioRepositorio.Dispose();
                _vaquinhaRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
