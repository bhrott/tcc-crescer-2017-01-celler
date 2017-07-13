using System;
using System.Collections.Generic;

namespace Celler.Dominio.Entidades
{
    public class Evento : Anuncio
    {
        public DateTime DataRealizacao { get; private set; }
        public string Local { get; private set; }
        public DateTime DataMaximaConfirmacao { get; private set; }
        public double ValorPorPessoa { get; private set; }
        public List<Usuario> Confirmados { get; set; }

        protected Evento() { }

        public static readonly string Erro_Usuario_Ja_Confirmado = "Usuário já está confirmado neste evento.";
        public static readonly string Erro_Proprio_Evento = "O criador não pode confirmar presença no próprio produto";
        public static readonly string Erro_Evento_Ocorrido = "Você não pode confirmar presença num evento já ocorrido";
        public static readonly string Erro_Usuario_Nao_Interessado = "Usuário não está interessado neste evento.";

        public override int GetNumeroPessoasComInteresse()
        {
            return Confirmados.Count;
        }

        public void AdicionarInteressado(Usuario usuario)
        {
            if (Confirmados.Contains(usuario))
                AdicionarMensagem(Erro_Usuario_Ja_Confirmado);

            else if (Criador.Equals(usuario))
                AdicionarMensagem(Erro_Proprio_Evento);

            else if (Status == "f")
                AdicionarMensagem(Erro_Evento_Ocorrido);

            else
                Confirmados.Add(usuario);

        }

        public void RemoverInteressado(Usuario usuario)
        {
            if (!Confirmados.Contains(usuario))
                AdicionarMensagem(Erro_Usuario_Nao_Interessado);

            Confirmados.Remove(usuario);
        }
    }
}
