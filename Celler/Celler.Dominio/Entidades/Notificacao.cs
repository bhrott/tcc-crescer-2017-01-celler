using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Notificacao
    {
        public int Id { get; private set; }
        public string Texto { get; private set; }
        // Opções status: 'l' - lido; 'n' - não lido;
        public string Status { get; private set; }
        public Usuario Usuario { get; private set; }
        public string Link { get; private set; }

        protected Notificacao() { }

        public Notificacao(string texto, Usuario usuario, string link)
        {
            Texto = texto;
            Status = "n";
            Usuario = usuario;
            Link = link;
        }
    }
}
