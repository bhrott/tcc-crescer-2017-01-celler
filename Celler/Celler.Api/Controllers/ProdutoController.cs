﻿using Celler.Api.App_Start;
using Celler.Api.Models;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Celler.Api.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/produto")]

    public class ProdutoController : ControllerBasica
    {
        readonly ProdutoRepositorio _produtoRepositorio;
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly Contexto _contexto = new Contexto();

        public ProdutoController()
        {
            _produtoRepositorio = new ProdutoRepositorio(_contexto);
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
        }

        [HttpPost, Route("interessar")]
        public HttpResponseMessage SalvarInteressadoProduto([FromBody] InteressarProdutoModel model)
        {
            var usuario = _usuarioRepositorio.ObterPorId(model.IdUsuario);
            var produto = _produtoRepositorio.ObterPorId(model.IdProduto);

            if (usuario == null || produto == null)
            {
                return ResponderErro("Usuario ou Produto inválidos.");
            }

            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            if (usuarioLogado.Equals(produto.Criador))
            {
                return ResponderErro("Você não pode manifestar interesse no próprio anúncio.");
            }

            produto.AdicionarInteressado(usuarioLogado);
            
            if (produto.Validar())
            {
                _produtoRepositorio.Alterar(produto);
                _contexto.SaveChanges();
                return ResponderOk(new { texto = "Interesse salvo com sucesso" });
            }
            else
            {
                return ResponderErro(produto.Mensagens);
            }            
        }

        [HttpPost, Route("desinteressar")]
        public HttpResponseMessage SalvarDesinteressarProduto([FromBody] InteressarProdutoModel model)
        {
            var usuario = _usuarioRepositorio.ObterPorId(model.IdUsuario);
            var produto = _produtoRepositorio.ObterPorId(model.IdProduto);

            if (usuario == null || produto == null)
            {
                return ResponderErro("Usuario ou Produto inválidos.");
            }

            var usuarioLogado = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            if (usuarioLogado.Equals(produto.Criador))
            {
                return ResponderErro("Você não pode desmanifestar interesse no próprio anúncio.");
            }

            produto.RemoverInteressado(usuarioLogado);

            if (produto.Validar())
            {
                _produtoRepositorio.Alterar(produto);
                _contexto.SaveChanges();
                return ResponderOk(new { texto = "Desinteresse salvo com sucesso" });
            }
            else
            {
                return ResponderErro(produto.Mensagens);
            }
        }
    }
}
