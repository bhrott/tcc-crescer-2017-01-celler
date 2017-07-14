using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celler.Tests
{
    [TestClass]
    public class AnuncioTest
    {
        [TestMethod]
        public void AnuncioSemTitulo ()
        {
            Usuario usuario = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("", "Descricao", null, null, null, usuario, 50.0);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Anuncio.Erro_Sem_Titulo));
        }

        [TestMethod]
        public void AnuncioSemDescricao()
        {
            Usuario usuario = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "", null, null, null, usuario, 50.0);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Anuncio.Erro_Sem_Descricao));
        }

        [TestMethod]
        public void AnuncioSemCriador()
        {
            Usuario usuario = new Usuario("Logado", "Logado", "Senha");
            Produto produto = new Produto("Titulo", "Descricao", null, null, null, null, 50.0);
            Assert.IsFalse(produto.Validar());
            Assert.IsTrue(produto.Mensagens.Contains(Anuncio.Erro_Sem_Criador));
        }
    }
}
