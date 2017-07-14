using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celler.Tests.Testes
{
    [TestClass]
    public class DoadorTest
    {
        [TestMethod]
        public void CriacaoDoacaoOk()
        {
            Usuario usuario = new Usuario("Titulo","Descricao", "Senha");
            Doador doador = new Doador(usuario, 20.0);
            Assert.IsTrue(doador.Validar());
        }

        [TestMethod]
        public void ValorDoadoZero()
        {
            Usuario usuario = new Usuario("Titulo", "Descricao", "Senha");
            Doador doador = new Doador(usuario, 0.0);
            Assert.IsFalse(doador.Validar());
            Assert.IsTrue(doador.Mensagens.Contains(Doador.Erro_Valor_Doado_Zero));
        }

        [TestMethod]
        public void ValorDoadoNegativo()
        {
            Usuario usuario = new Usuario("Titulo", "Descricao", "Senha");
            Doador doador = new Doador(usuario, -20.0);
            Assert.IsFalse(doador.Validar());
            Assert.IsTrue(doador.Mensagens.Contains(Doador.Erro_Valor_Doado_Negativo));
        }
    }
}
