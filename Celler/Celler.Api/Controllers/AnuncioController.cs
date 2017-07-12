using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Dominio.Entidades;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/anuncio")]

    public class AnuncioController : ControllerBasica
    {
        readonly AnuncioRepositorio _anuncioRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;
        ProdutoRepositorio _produtoRepositorio;
        EventoRepositorio _eventoRepositorio;
        //VaquinhaRepositorio _vaquinhaRepositorio;
        readonly Contexto _contexto = new Contexto();

        public AnuncioController()
        {
            _anuncioRepositorio = new AnuncioRepositorio(_contexto);
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
            _eventoRepositorio = new EventoRepositorio(_contexto);
            _produtoRepositorio = new ProdutoRepositorio(_contexto);
            //_vaquinhaRepositorio = new UsuarioRepositorio(_contexto);
        }

        [HttpGet, Route("feed/{pagina:int}")]
        public HttpResponseMessage ObterUltimosAnuncios(int pagina)
        {
            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina);
            return ResponderOk(anuncios );
        }

        [HttpGet, Route("feed")]
        public HttpResponseMessage ObterUltimosAnunciosFiltrados(int pagina, string filtro1 = null, string filtro2 = null, string filtro3 = null, string search = null)
        {
            if (filtro1 == null)
            {
                filtro1 = "Evento";
                filtro2 = "Produto";
                filtro3 = "Vaquinha";

            }

            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina, filtro1, filtro2, filtro3,search);
            return ResponderOk(anuncios);
        }

        [HttpGet, Route("{id:int}")]
        public HttpResponseMessage ObterDetalhesAnuncio(int id)
        {
            var anuncio = _anuncioRepositorio.ObterCompleto(id);
            var usuarioQuePostouAnuncio = _anuncioRepositorio.Obter(anuncio.Criador.Id);
            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            bool isUsuarioLogado = usuarioQuePostouAnuncio.Id == usuarioLogado.Id;

            object anuncioDetalhes = _anuncioRepositorio.ObterDetalhesAnuncio(anuncio, isUsuarioLogado, ref _produtoRepositorio, ref _eventoRepositorio);

            return ResponderOk(anuncioDetalhes);
        }

        [HttpGet, Route("comentarios")]
        public HttpResponseMessage ObterAnuncioPorId(int id, int pagina)
        {
            var anuncio = _anuncioRepositorio.ObterComentariosPorId(id, pagina);
            return ResponderOk(anuncio);
        }

        [HttpPost, Route("comentar")]
        public HttpResponseMessage ComentarAnuncio(ComentarioModel model)
        {
            Usuario usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            Anuncio anuncio = _anuncioRepositorio.Obter(model.IdAnuncio);

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
                //_vaquinhaRepositorio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
