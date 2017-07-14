using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celler.Tests
{
    [TestClass]
    public class ComentarioTest
    {
        Usuario usuario;
        Comentario comentarioCorreto;
        Comentario comentarioTextoNulo;
        Comentario comentarioTextoVazio;

        [TestMethod]
        public void ComenarioCorretoEhValidado()
        {
            usuario = new Usuario("Robson", "robson@user.com", "hash");
            comentarioCorreto = new Comentario("Comentario bombante", usuario);

            Assert.IsTrue(comentarioCorreto.Validar());
        }

        [TestMethod]
        public void ComenarioNuloEhInvalidado()
        {
            usuario = new Usuario("Robson", "robson@user.com", "hash");
            comentarioTextoNulo = new Comentario(null, usuario);

            Assert.IsFalse(comentarioTextoNulo.Validar());
        }

        [TestMethod]
        public void ComenarioVazioEhInvalidado()
        {
            usuario = new Usuario("Robson", "robson@user.com", "hash");
            comentarioTextoVazio = new Comentario("" ,usuario);

            Assert.IsFalse(comentarioTextoVazio.Validar());
        }
    }
}
