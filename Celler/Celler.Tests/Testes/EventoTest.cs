using Celler.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Celler.Tests.Testes
{
    [TestClass]
    public class EventoTest
    {
        [TestMethod]
        public void EventoCorretoOk()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioLogado, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            Assert.IsTrue(evento.Validar());
        }

        [TestMethod]
        public void ConfirmarParticipacaoOk()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.AdicionarInteressado(usuarioLogado);
            Assert.IsTrue(evento.Validar());
        }

        [TestMethod]
        public void RemoverParticipacaoOk()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.AdicionarInteressado(usuarioLogado);
            evento.RemoverInteressado(usuarioLogado);
            Assert.IsTrue(evento.Validar());
        }

        [TestMethod]
        public void DataConfirmacaoMaiorQueRealizacaoErro()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioLogado, "CWI", (DateTime.MaxValue).AddDays(-1), DateTime.MaxValue, 20.0);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Data_Confirmacao_Maior_Data_Realizacao));
        }

        [TestMethod]
        public void ValorPorPessoaENegativo()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioLogado, "CWI", (DateTime.MaxValue).AddDays(-1), DateTime.MaxValue, -20.0);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Valor_Por_Pessoa_Negativo));
        }

        [TestMethod]
        public void LocalEstaVazio()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioLogado, "", (DateTime.MaxValue).AddDays(-1), DateTime.MaxValue, 20.0);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Local_Vazio));
        }

        [TestMethod]
        public void UsuarioTentaConfirmarDuasVezesErro()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.AdicionarInteressado(usuarioLogado);
            Assert.IsTrue(evento.Validar());
            evento.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Usuario_Ja_Confirmado));
        }

        [TestMethod]
        public void ConfirmarPresencaProprioEventoErro()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioLogado, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Proprio_Evento));
        }

        [TestMethod]
        public void DesinteressarQuandoNaoInteressadoErro()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", DateTime.MaxValue, DateTime.MaxValue, 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.RemoverInteressado(usuarioLogado);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Usuario_Nao_Interessado));
        }

        [TestMethod]
        public void TentarSeInteressarDepoisDaDataMaximaErro()
        {
            Usuario usuarioLogado = new Usuario("Logado", "Logado", "Senha");
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", new DateTime(2014, 10, 18), new DateTime(2014, 10, 18), 20.0);
            evento.Confirmados = new List<Usuario>();
            evento.AdicionarInteressado(usuarioLogado);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Evento_Data_Maxima));
        }

        [TestMethod]
        public void DataRealizacaoJaPassou()
        {
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", new DateTime(2014, 10, 18), new DateTime(2014, 10, 08), 20.0);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Data_Realizacao_Ja_Passou));
        }

        [TestMethod]
        public void DataMaximaConfirmacaoJaPassou()
        {
            Usuario usuarioOutro = new Usuario("Outro", "Outro", "Senha");
            Evento evento = new Evento("Titulo", "Descricao", null, null, null, usuarioOutro, "CWI", new DateTime(2014, 10, 18), new DateTime(2014, 10, 08), 20.0);
            Assert.IsFalse(evento.Validar());
            Assert.IsTrue(evento.Mensagens.Contains(Evento.Erro_Data_Confirmacao_Ja_Passou));
        }
    }
}
