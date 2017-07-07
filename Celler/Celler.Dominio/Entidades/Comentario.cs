using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Comentario
    {
        public int Id { get; private set; }
        public string Texto { get; private set; }

        public Comentario(){}
    }
}
