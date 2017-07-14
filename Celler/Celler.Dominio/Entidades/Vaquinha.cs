using System;
using System.Collections.Generic;

namespace Celler.Dominio.Entidades
{
    public class Vaquinha : Anuncio
    {
        public double ArrecadamentoPrevisto { get; private set; }
        public double TotalArrecadado { get; private set; }
        public DateTime DateTermino { get; private set; }
        public List<Doador> Doadores { get; set; }

        protected Vaquinha() { }

        public Vaquinha(string titulo, string descricao, string foto1, string foto2, string foto3, Usuario usuarioLogado, double arrecadamentoPrevisto, DateTime dataTermino)
            :base(titulo, descricao, usuarioLogado, Entidades.TipoAnuncio.VAQUINHA, foto1, foto2, foto3)
        {
            if (dataTermino < DateTime.Now)
                AdicionarMensagem(Erro_Data_Termino_Ja_Passou);

            if(arrecadamentoPrevisto < 0)
                AdicionarMensagem(Erro_Arrecadamento_Previsto_Negativo);

            if (arrecadamentoPrevisto == 0)
                AdicionarMensagem(Erro_Arrecadamento_Previsto_Zerado);

            ArrecadamentoPrevisto = arrecadamentoPrevisto;
            DateTermino = dataTermino;
            TotalArrecadado = 0.0;
        }

        public static readonly string Erro_Vaquinha_Fechada = "Você não pode doar numa vaquinha fechada";
        public static readonly string Erro_Data_Termino_Ja_Passou = "A data de término é inválida.";
        public static readonly string Erro_Arrecadamento_Previsto_Negativo = "O arrecadamento previsto não pode ser negativo.";
        public static readonly string Erro_Arrecadamento_Previsto_Zerado = "O arrecadamento previsto não pode ser zero.";

        public override int GetNumeroPessoasComInteresse()
        {
            return Doadores.Count;
        }

        public void AdicionarDoador(Doador doador)
        { 
            if (Status == "f" || DateTermino < DateTime.Now)
                AdicionarMensagem(Erro_Vaquinha_Fechada);
            
            Doadores.Add(doador);
        }

        public void IncrementarTotal(double valorRecebido)
        {
            this.TotalArrecadado += valorRecebido;
        }
    }
}
