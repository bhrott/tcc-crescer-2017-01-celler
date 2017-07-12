using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Doador
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; set; }
        public double ValorDoado { get; private set; }
        //Status: 'p' - pago; 'n' - não pago
        public string Status { get; private set; }

        protected Doador(){}

        public Doador(Usuario usuario, double valorDoado)
        {
            this.Usuario = usuario;
            this.ValorDoado = valorDoado;
            this.Status = "n";
        }
    }
}
