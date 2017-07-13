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

        public static readonly string Erro_Vaquinha_Fechada = "Você não pode doar numa vaquinha fechada";

        public override int GetNumeroPessoasComInteresse()
        {
            return Doadores.Count;
        }

        public void AdicionarDoador(Doador doador)
        {
            if (Status == "f")
                AdicionarMensagem(Erro_Vaquinha_Fechada);
            else
                Doadores.Add(doador);
        }

        public void IncrementarTotal(double valorRecebido)
        {
            this.TotalArrecadado += valorRecebido;
        }
    }
}
