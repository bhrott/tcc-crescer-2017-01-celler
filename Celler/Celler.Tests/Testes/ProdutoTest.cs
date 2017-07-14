using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Celler.Tests
{
    [TestClass]
    public class ProdutoTest
    {
        [TestMethod]
        public void ProdutoCorretoOk ()
        {
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, new Usuario("Nome", "Email", "Senha"), 50.0);
            Assert.IsTrue(produto.Validar());
        }

        [TestMethod]
        public void InteressarProdutoOk()
        {
            Usuario usuario = new Usuario("Nome", "Email", "Senha");
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuario, 50.0);
            produto.Interessados = new List<Usuario>();
            produto.AdicionarInteressado(usuarioLogado);
            Assert.IsTrue(produto.Validar());
        }

        [TestMethod]
        public void DesinteressarProdutoOk()
        {
            Usuario usuario = new Usuario("Nome", "Email", "Senha");
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuario, 50.0);
            produto.Interessados = new List<Usuario>();
            produto.AdicionarInteressado(usuarioLogado);
            produto.RemoverInteressado(usuarioLogado);
            Assert.IsTrue(produto.Validar());
        }

        [TestMethod]
        public void ProdutoUsuarioJaInteressado()
        {
            Usuario usuario = new Usuario("Nome", "Email", "Senha");
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuario, 50.0);
            produto.Interessados = new List<Usuario>();
            produto.AdicionarInteressado(usuarioLogado);
            Assert.IsTrue(produto.Validar());
            produto.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Produto.Erro_Usuario_Ja_Interessado));
        }

        [TestMethod]
        public void ProdutoUsuarioInteressadoProprioProduto()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuarioLogado, 50.0);
            produto.Interessados = new List<Usuario>();
            produto.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Produto.Erro_Proprio_Produto));
        }

        [TestMethod]
        public void ProdutoUsuarioNaoInteressado()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuarioLogado, 50.0);
            produto.Interessados = new List<Usuario>();
            produto.RemoverInteressado(usuarioLogado);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Produto.Erro_Usuario_Nao_Interessado));
        }

        [TestMethod]
        public void ProdutoPrecoInferiorMinimo()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, usuarioLogado, 2.0);
            produto.Interessados = new List<Usuario>();
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Produto.Erro_Preco_Inferior_5Reais));
        }
    }
}
