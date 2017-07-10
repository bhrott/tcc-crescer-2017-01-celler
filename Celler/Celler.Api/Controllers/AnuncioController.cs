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
        public IHttpActionResult ObterUltimosAnuncios(int pagina)
        {
            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina);
            return Ok(new { dados = anuncios });
        }

        [HttpGet, Route("feed")]
        public IHttpActionResult ObterUltimosAnunciosFiltrados(int pagina, string filtro1, string filtro2 = null, string filtro3 = null, string search = null)
        {
            var anuncios = _anuncioRepositorio.ObterUltimosAnuncios(pagina, filtro1, filtro2, filtro3,search);
            return Ok(new { dados = anuncios });
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult ObterAnuncioPorId(int id)
        {
            var anuncio = _anuncioRepositorio.ObterAnuncioPorId(id);
            return Ok(new { dados = anuncio });
        }

        [HttpPost, Route("comentar")]
        public HttpResponseMessage ComentarAnuncio(ComentarioModel model)
        {
            if (!model.Validar())
            {
                return ResponderErro(model.Mensagens);
            }


            Usuario usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);
            _anuncioRepositorio.ComentarAnuncio(model.Texto, model.IdAnuncio, usuario);

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
