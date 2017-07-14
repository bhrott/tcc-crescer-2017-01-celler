using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Celler.Tests.Testes
{
    [TestClass]
    public class VaquinhaTest
    {
        //public static readonly string Erro_Vaquinha_Fechada = "Você não pode doar numa vaquinha fechada";
        //public static readonly string Erro_Data_Termino_Ja_Passou = "A data de término é inválida.";
        //public static readonly string Erro_Arrecadamento_Previsto_Negativo = "O arrecadamento previsto não pode ser negativo.";
        //public static readonly string Erro_Arrecadamento_Previsto_Zerado = "O arrecadamento previsto não pode ser zero.";

        [TestMethod]
        public void VaquinhaCorretoOk()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Vaquinha vaquinha = new Vaquinha("Titulo", "Descricao", null, null, null, usuarioLogado, 500.0, DateTime.MaxValue);
            Assert.IsTrue(vaquinha.Validar());
        }

        [TestMethod]
        public void DoarVaquinhaFechada()
        {
            Usuario usuario = new Usuario("Titulo", "Descricao", "Senha");
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Doador doador = new Doador(usuarioLogado, 20.0);
            Vaquinha vaquinha = new Vaquinha("Titulo", "Descricao", null, null, null, usuario, 500.0, new DateTime(2014, 01, 01));
            vaquinha.Doadores = new List<Doador>();
            vaquinha.AdicionarDoador(doador);
            Assert.IsFalse(vaquinha.Validar());
            Assert.IsTrue(vaquinha.Mensagens.Contains(Vaquinha.Erro_Vaquinha_Fechada));
        }

        [TestMethod]
        public void DoarVaquinhaDataJaPassou()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Vaquinha vaquinha = new Vaquinha("Titulo", "Descricao", null, null, null, usuarioLogado, 500.0, new DateTime(2014,01,01));
            Assert.IsFalse(vaquinha.Validar());
            Assert.IsTrue(vaquinha.Mensagens.Contains(Vaquinha.Erro_Data_Termino_Ja_Passou));
        }

        [TestMethod]
        public void ArrecadamentoNegativo()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Vaquinha vaquinha = new Vaquinha("Titulo", "Descricao", null, null, null, usuarioLogado, -500.0, DateTime.MaxValue);
            Assert.IsFalse(vaquinha.Validar());
            Assert.IsTrue(vaquinha.Mensagens.Contains(Vaquinha.Erro_Arrecadamento_Previsto_Negativo));
        }

        [TestMethod]
        public void ArrecadamentoZerado()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Vaquinha vaquinha = new Vaquinha("Titulo", "Descricao", null, null, null, usuarioLogado, 0.0, DateTime.MaxValue);
            Assert.IsFalse(vaquinha.Validar());
            Assert.IsTrue(vaquinha.Mensagens.Contains(Vaquinha.Erro_Arrecadamento_Previsto_Zerado));
        }
    }
}
