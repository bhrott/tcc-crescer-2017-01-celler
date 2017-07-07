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
        public char Status { get; private set; }
        public Usuario Usuario { get; private set; }

        public Notificacao() { }
    }
}
