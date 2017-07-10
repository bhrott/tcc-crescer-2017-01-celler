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
    [RoutePrefix("api/produto")]

    public class ProdutoController : ApiController
    {
        readonly ProdutoRepositorio _produtoRepositorio;
        readonly Contexto _contexto = new Contexto();

        public ProdutoController()
        {
            _produtoRepositorio = new ProdutoRepositorio(_contexto);

        }

        [HttpPost, Route("interessado")]
        public IHttpActionResult SalvarInteressadoProduto(int idUsuario, int idProduto)
        {
            _produtoRepositorio.SalvarInteressadoProduto(idUsuario, idProduto);
            return Ok();
        }
    }
}
