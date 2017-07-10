using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Dominio.Entidades;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System;
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

    public class AnuncioController : ApiController
    {
        readonly AnuncioRepositorio _anuncioRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;

        public AnuncioController()
        {
            _anuncioRepositorio = new AnuncioRepositorio();
            _usuarioRepositorio  = new UsuarioRepositorio();
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

        [HttpPost, Route("comentar")]
        public IHttpActionResult ComentarAnuncio(ComentarioModel model)
        {
            model.Validar();

            Usuario usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            var resposta = _anuncioRepositorio.ComentarAnuncio(model.Texto, model.IdAnuncio, usuario);
          
            return Ok(new { dados = resposta });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _usuarioRepositorio.Dispose();
            _anuncioRepositorio.Dispose();
        }
    }
}
