using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Dominio.Entidades;
using Celler.Dominio.Models;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/anuncio")]
    public class AnuncioController : ControllerBasica
    {
        readonly AnuncioRepositorio _anuncioRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly ProdutoRepositorio _produtoRepositorio;
        readonly EventoRepositorio _eventoRepositorio;
        readonly VaquinhaRepositorio _vaquinhaRepositorio;
        readonly Contexto _contexto = new Contexto();

        public AnuncioController()
        {
            _anuncioRepositorio = new AnuncioRepositorio(_contexto);
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
            _eventoRepositorio = new EventoRepositorio(_contexto);
            _produtoRepositorio = new ProdutoRepositorio(_contexto);
            _vaquinhaRepositorio = new VaquinhaRepositorio(_contexto);
        }

        [HttpGet, Route("feed/{pagina:int}")]
        public HttpResponseMessage ObterUltimosAnuncios(int pagina)
        {
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina, usuarioLogado);
            return ResponderOk(anuncios );
        }

        [HttpGet, Route("feed")]
        public HttpResponseMessage ObterUltimosAnunciosFiltrados(int pagina, string filtro1 = null, string filtro2 = null, string filtro3 = null, string search = null)
        {
            if (filtro1 == null)
            {
                filtro1 = TipoAnuncio.EVENTO;
                filtro2 = TipoAnuncio.PRODUTO;
                filtro3 = TipoAnuncio.VAQUINHA;

            }

            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina, filtro1, filtro2, filtro3,search, usuarioLogado);
            return ResponderOk(anuncios);
        }

        [HttpGet, Route("{id:int}")]
        public HttpResponseMessage ObterDetalhesAnuncio(int id)
        {
            var anuncio = _anuncioRepositorio.ObterCompleto(id);
            var usuarioQuePostouAnuncio = _anuncioRepositorio.Obter(anuncio.Criador.Id);
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            bool isUsuarioLogado = usuarioQuePostouAnuncio.Id == usuarioLogado.Id;

            if (anuncio == null)
            {
                ResponderErro("Anuncio não encontrado");
            }

            AnuncioModelDetalhes anuncioDetalhes;
            switch (anuncio.TipoAnuncio)
            {
                case TipoAnuncio.PRODUTO:
                    anuncioDetalhes = _produtoRepositorio.ObterDetalhes(anuncio.Id, isUsuarioLogado);
                    return ResponderOk(anuncioDetalhes);

                case TipoAnuncio.EVENTO:
                    anuncioDetalhes = _eventoRepositorio.ObterDetalhes(anuncio.Id);
                    return ResponderOk(anuncioDetalhes);

                case TipoAnuncio.VAQUINHA:
                    anuncioDetalhes = _vaquinhaRepositorio.ObterDetalhes(anuncio.Id, isUsuarioLogado);
                    return ResponderOk(anuncioDetalhes);

                default:
                    break;
            }
            
            return ResponderErro("Anuncio não encontrado");
        }

        [HttpGet, Route("comentarios")]
        public HttpResponseMessage ObterAnuncioPorId(int id, int pagina)
        {
            var anuncio = _anuncioRepositorio.ObterComentariosPorId(id, pagina);
            return ResponderOk(anuncio);
        }

        [HttpPost, Route("comentar")]
        public HttpResponseMessage ComentarAnuncio(ComentarioModelDetalhes model)
        {
            Usuario usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            Anuncio anuncio = _anuncioRepositorio.ObterCompleto(model.IdAnuncio);

            if (usuario == null || anuncio == null)
            {
                return ResponderErro("Anuncio ou usuario inválido");
            }

            Comentario comentario = new Comentario(model.Texto, usuario);
            anuncio.AdicionarComentario(comentario);

            if (comentario.Validar() && anuncio.Validar())
            {
                _anuncioRepositorio.Alterar(anuncio);
                _contexto.SaveChanges();
                return ResponderOk(new { texto = model.Texto });
            }
            else
                return ResponderErro(comentario.Mensagens);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _anuncioRepositorio.Dispose();
                _usuarioRepositorio.Dispose();
                _produtoRepositorio.Dispose();
                _eventoRepositorio.Dispose();
                _vaquinhaRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
