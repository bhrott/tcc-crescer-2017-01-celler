using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celler.Dominio.Entidades;
using Celler.Api.Models;

namespace Celler.Tests
{
    [TestClass]
    public class ComentarioTest
    {
        ComentarioModel comentarioCorreto;
        ComentarioModel comentarioTextoNulo;
        ComentarioModel comentarioTextoVazio;

        [TestMethod]
        public void ComenarioCorretoEhValidado()
        {
            comentarioCorreto = new ComentarioModel();
            comentarioCorreto.Texto = "Comentario bombante";
            comentarioCorreto.IdAnuncio = 2;

            Assert.IsTrue(comentarioCorreto.Validar());
        }

        [TestMethod]
        public void ComenarioNuloEhInvalidado()
        {
            comentarioTextoNulo = new ComentarioModel();
            comentarioTextoNulo.Texto = null;
            comentarioTextoNulo.IdAnuncio = 2;

            Assert.IsFalse(comentarioTextoNulo.Validar());
        }

        [TestMethod]
        public void ComenarioVazioEhInvalidado()
        {
            comentarioTextoVazio = new ComentarioModel();
            comentarioTextoVazio.Texto = "";
            comentarioTextoVazio.IdAnuncio = 2;

            Assert.IsFalse(comentarioTextoVazio.Validar());
        }
    }
}
