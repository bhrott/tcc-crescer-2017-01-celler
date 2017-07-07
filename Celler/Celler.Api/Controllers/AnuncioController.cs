using Celler.Api.App_Start;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/anuncio")]

    public class AnuncioController : ApiController
    {
        private readonly AnuncioRepositorio repositorio = new AnuncioRepositorio();

        [HttpGet, Route("feed/{pagina:int}")]
        public IHttpActionResult ObterUltimosAnuncios(int pagina)
        {
            var anuncios = repositorio.ObterUltimosAnuncios(pagina);
            return Ok(new { dados = anuncios });
        }

        [HttpGet, Route("feed")]
        public IHttpActionResult ObterUltimosAnunciosFiltrados(string filtro1, string filtro2 = null, string filtro3 = null, string search = null)
        {
            var anuncios = repositorio.ObterUltimosAnuncios(filtro1, filtro2, filtro3,search);
            return Ok(new { dados = anuncios });
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult ObterAnuncioPorId(int id)
        {
            var anuncio = repositorio.ObterAnuncioPorId(id);
            return Ok(new { dados = anuncio });
        }
    }
}
