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
        readonly Contexto _contexto = new Contexto();

        public AnuncioController()
        {
            _anuncioRepositorio = new AnuncioRepositorio(_contexto);
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
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
        public HttpResponseMessage ObterAnuncioPorId(int id)
        {
            var anuncio = _anuncioRepositorio.ObterAnuncioPorId(id);
            return ResponderOk(anuncio);
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
            Anuncio anuncio = _contexto.Anuncio.FirstOrDefault(a => a.Id == model.IdAnuncio);

            if(usuario == null || anuncio == null)
            {
                return ResponderErro("Anuncio ou usuario inválido");
            }

            if (!model.Validar())
            {
                return ResponderErro(model.Mensagens);
            }

            if (!usuario.Validar())
            {
                return ResponderErro(model.Mensagens);
            }

            if (!anuncio.Validar())
            {
                return ResponderErro(model.Mensagens);
            }

            Comentario comentario = new Comentario(model.Texto, usuario, DateTime.Now);
            _anuncioRepositorio.ComentarAnuncio(anuncio, comentario);

            return ResponderOk(new { texto = model.Texto });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _usuarioRepositorio.Dispose();
            base.Dispose(disposing);
        }
    }
}
