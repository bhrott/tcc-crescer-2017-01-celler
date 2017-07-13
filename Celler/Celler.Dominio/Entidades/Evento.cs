using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override int GetNumeroPessoasComInteresse()
        {
            return Confirmados.Count;
        }

        public void AdicionarInteressado(Usuario usuario)
        {
            if (Confirmados.Contains(usuario))
                AdicionarMensagem("Usuário já está confirmado neste evento.");
            else if (Criador.Equals(usuario))
                AdicionarMensagem("O criador não pode confirmar presença no próprio produto");
            else if (Status == "f")
                AdicionarMensagem("Você não pode confirmar presença num evento já ocorrido");
            else
                Confirmados.Add(usuario);

        }

        public void RemoverInteressado(Usuario usuario)
        {
            if (!Confirmados.Contains(usuario))
                AdicionarMensagem("Usuário ainda não está confirmado neste evento.");

            Confirmados.Remove(usuario);
        }
    }
}
