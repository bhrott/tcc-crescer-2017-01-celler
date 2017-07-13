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

        public Evento (string titulo, string descricao, string foto1, string foto2, string foto3, Usuario usuarioLogado, 
                        DateTime DataRealizacao, DateTime DataMaximaConfirmacao,  double valorPessoa) 
            : base(titulo, descricao, usuarioLogado, Entidades.TipoAnuncio.EVENTO, foto1, foto2, foto3)
        {
            if (DataRealizacao < DataMaximaConfirmacao)
                AdicionarMensagem(Erro_Data_Confirmacao_Maior_Data_Realizacao);

            if (ValorPorPessoa < 0)
                AdicionarMensagem(Erro_Valor_Por_Pessoa_Negativo);

            if (string.IsNullOrEmpty(Local))
                AdicionarMensagem(Erro_Data_Confirmacao_Maior_Data_Realizacao);
        }

        public static readonly string Erro_Usuario_Ja_Confirmado = "Usuário já está confirmado neste evento.";
        public static readonly string Erro_Proprio_Evento = "O criador não pode confirmar presença no próprio produto";
        public static readonly string Erro_Evento_Data_Maxima = "Você não pode confirmar presença depois da data limite";
        public static readonly string Erro_Usuario_Nao_Interessado = "Usuário não está interessado neste evento.";
        public static readonly string Erro_Data_Confirmacao_Maior_Data_Realizacao = "A data máxima para confirmação não pode ser maior que a data de realização.";
        public static readonly string Erro_Valor_Por_Pessoa_Negativo = "O valor por pessoa não pode ser negativo";
        public static readonly string Erro_Local_Vazio = "O local precisa ser informado";

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

            else if (Status == "f" || DataRealizacao > DateTime.Now)
                AdicionarMensagem(Erro_Evento_Data_Maxima);

            else if (DataRealizacao < DataMaximaConfirmacao)
                AdicionarMensagem(Erro_Data_Confirmacao_Maior_Data_Realizacao);

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
