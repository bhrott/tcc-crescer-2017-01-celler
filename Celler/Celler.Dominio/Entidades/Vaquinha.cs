
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Vaquinha : Anuncio
    {
        public double ArrecadamentoPrevisto { get; private set; }
        public double TotalArrecadado { get; private set; }
        public DateTime DateTermino { get; private set; }
        public List<Doador> Doadores { get; set; }

        protected Vaquinha() { }

        public override int GetNumeroPessoasComInteresse()
        {
            return Doadores.Count;
        }

        public void AdicionarDoador(Doador doador)
        {
            if (Status == "f")
                AdicionarMensagem("Você não pode doar numa vaquinha fechada");
            else
                Doadores.Add(doador);
        }

        public void IncrementarTotal(double valorRecebido)
        {
            this.TotalArrecadado += valorRecebido;
        }
    }
}
