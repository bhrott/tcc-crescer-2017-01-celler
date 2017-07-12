using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class DoadorModel
    {
        public DoadorModel(int id, double valorDoado, string status, UsuarioModel usuario)
        {
            Id = id;
            Usuario = usuario;
            ValorDoado = valorDoado;
            Status = status;
        }

        public int Id { get; private set; }
        public UsuarioModel Usuario { get; set; }
        public double ValorDoado { get; private set; }
        //Status: 'p' - pago; 'n' - não pago
        public string Status { get; private set; }
    }
}
