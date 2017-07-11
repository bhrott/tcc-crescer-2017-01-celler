using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public abstract class EntidadeBasica
    {
        public List<string> Mensagens { get; private set; }

        public EntidadeBasica()
        {
            Mensagens = new List<string>();
        }

        public void AdicionarMensagem(string mensagemErro)
        {
            this.Mensagens.Add(mensagemErro);
        }

        public bool Validar()
        {
            return this.Mensagens.Count == 0;
        }
    }
}
