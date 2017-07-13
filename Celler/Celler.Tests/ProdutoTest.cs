using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            produtoCorreto = new Produto("Titulo", "Descricao", null, null, null, usuario, 50.0);
            produtoCorreto.Interessados = new List<Usuario>();
            produtoCorreto.AdicionarInteressado(usuarioLogado);
            Assert.IsTrue(produtoCorreto.Validar());
            produtoCorreto.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(produtoCorreto.Validar());
            Assert.IsTrue(produtoCorreto.Mensagens.Contains(Produto.Erro_Usuario_Ja_Interessado));
        }
    }
}
