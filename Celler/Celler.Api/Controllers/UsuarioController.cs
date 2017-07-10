using Celler.Api.App_Start;
using Celler.Infraestrutura;
using Celler.Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Celler.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/usuario")]

    public class UsuarioController : ApiController
    {
        readonly UsuarioRepositorio _usuarioRepositorio;
        readonly Contexto _contexto = new Contexto();

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio(_contexto);
        }

        [BasicAuthorization]
        [HttpGet]
        public HttpResponseMessage Obter()
        {
            var usuario = _usuarioRepositorio.Obter(Thread.CurrentPrincipal.Identity.Name);

            if (usuario == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagem = "Usuario/Email não cadastrados." });
            return Request.CreateResponse(HttpStatusCode.OK, new { dados = new { Nome = usuario.Nome, Email = usuario.Email } });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _usuarioRepositorio.Dispose();
            base.Dispose(disposing);
        }

    }
}

