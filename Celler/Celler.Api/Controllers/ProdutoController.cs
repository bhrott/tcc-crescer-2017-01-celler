using Celler.Api.App_Start;
using Celler.Api.Models;
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

    public class ProdutoController : ControllerBasica
    {
        readonly ProdutoRepositorio _produtoRepositorio;
        readonly Contexto _contexto = new Contexto();

        public ProdutoController()
        {
            _produtoRepositorio = new ProdutoRepositorio(_contexto);
        }

        [HttpPost, Route("interessado")]
        public HttpResponseMessage SalvarInteressadoProduto(InteressarProdutoModel produtoModel)
        {
            if (!produtoModel.Validar())
            {
                return ResponderErro(produtoModel.Mensagens);
            }

            var idUsuario = produtoModel.idUsuario;
            var idProduto = produtoModel.IdProduto;

            _produtoRepositorio.SalvarInteressadoProduto(idUsuario, idProduto);
            return ResponderOk(new { texto = "Interesse salvo com sucesso"});
        }
    }
}
