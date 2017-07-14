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
                        string local, DateTime dataRealizacao, DateTime dataMaximaConfirmacao,  double valorPessoa) 
            : base(titulo, descricao, usuarioLogado, Entidades.TipoAnuncio.EVENTO, foto1, foto2, foto3)
        {
            if (dataRealizacao < dataMaximaConfirmacao)
                AdicionarMensagem(Erro_Data_Confirmacao_Maior_Data_Realizacao);

            if (valorPessoa < 0)
                AdicionarMensagem(Erro_Valor_Por_Pessoa_Negativo);

            if (string.IsNullOrEmpty(local))
                AdicionarMensagem(Erro_Local_Vazio);

            if (dataRealizacao < dataMaximaConfirmacao)
                AdicionarMensagem(Erro_Data_Confirmacao_Maior_Data_Realizacao);

            if (dataRealizacao < DateTime.Now)
                AdicionarMensagem(Erro_Data_Realizacao_Ja_Passou);

            if (dataMaximaConfirmacao < DateTime.Now)
                AdicionarMensagem(Erro_Data_Confirmacao_Ja_Passou);

            else
            {
                DataRealizacao = dataRealizacao;
                Local = local;
                DataMaximaConfirmacao = dataMaximaConfirmacao;
                ValorPorPessoa = valorPessoa;
            }
        }

        public static readonly string Erro_Usuario_Ja_Confirmado = "Usuário já está confirmado neste evento.";
        public static readonly string Erro_Proprio_Evento = "O criador não pode confirmar presença no próprio produto";
        public static readonly string Erro_Evento_Data_Maxima = "Você não pode confirmar presença depois da data limite";
        public static readonly string Erro_Usuario_Nao_Interessado = "Usuário não está interessado neste evento.";
        public static readonly string Erro_Data_Confirmacao_Maior_Data_Realizacao = "A data máxima para confirmação não pode ser maior que a data de realização.";
        public static readonly string Erro_Valor_Por_Pessoa_Negativo = "O valor por pessoa não pode ser negativo";
        public static readonly string Erro_Local_Vazio = "O local precisa ser informado";
        public static readonly string Erro_Data_Realizacao_Ja_Passou = "A data de realização é inválida.";
        public static readonly string Erro_Data_Confirmacao_Ja_Passou = "A data máxima para confirmação é inválida.";

        public override int GetNumeroPessoasComInteresse()
        {
            return Confirmados.Count;
        }

        public void AdicionarInteressado(Usuario usuario)
        {
            if (Confirmados.Contains(usuario))
                AdicionarMensagem(Erro_Usuario_Ja_Confirmado);

            if (Criador.Equals(usuario))
                AdicionarMensagem(Erro_Proprio_Evento);

            if (Status == "f" || DataMaximaConfirmacao < DateTime.Now)
                AdicionarMensagem(Erro_Evento_Data_Maxima);

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
