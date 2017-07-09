using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Produto : Anuncio
    {
        public double Valor { get; private set; }
        //Status: 'v' - vendido; 'a' - anunciado; 'd' - deletado 
        public string Status { get; private set; }
        public Usuario Comprador { get; private set; }
        public List<Usuario> Interessados { get; private set; }

        public Produto(){ }
    }
}
