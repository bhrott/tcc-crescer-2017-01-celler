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
        public IHttpActionResult ObterUltimosAnunciosFiltrados(string filter1, string filter2 = null, string filter3 = null)
        {
            string[] filtros = new string[] { filter1, filter2, filter3 };

            var anuncios = repositorio.ObterUltimosAnuncios(filtros);
            return Ok(new { dados = anuncios });
        }
    }
}
