using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celler.Tests
{
    [TestClass]
    public class ProdutoTest
    {
        Produto produtoCorreto;
        Produto produtoUsuarioJaInteressado;
        Produto produtoUsuarioInteressadoProprioProduto;
        Produto produtoUsuarioNaoInteressado;
        Produto produtoPrecoInferiorMinimo;

        [TestMethod]
        public void ProdutoCorretoOk ()
        {
            produtoCorreto = new Produto("Titulo", "Descricao", null, null, null, new Usuario("Nome", "Email", "Senha"), 50.0);
            Assert.IsTrue(produtoCorreto.Validar());
        }

        [TestMethod]
        public void ProdutoUsuarioJaInteressado()
        {
            Usuario usuario = new Usuario("Nome", "Email", "Senha");
            produtoCorreto = new Produto("Titulo", "Descricao", null, null, null, usuario, 50.0);
            produtoCorreto.AdicionarInteressado(usuario);
            Assert.IsTrue(produtoCorreto.Validar());
            produtoCorreto.AdicionarInteressado(usuario);
            Assert.IsFalse(produtoCorreto.Validar());
        }
    }
}
