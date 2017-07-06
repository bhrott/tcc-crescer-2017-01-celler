using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; private set; }

        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public double Valor { get; private set; }

        //Status: 'v' - vendido; 'a' - anunciado; 'd' - deletado 
        public char Status { get; private set; }

        public Usuario Criador { get; private set; }

        public Usuario Comprador { get; private set; }

        public string Foto1 { get; private set; }

        public string Foto2 { get; private set; }

        public string Foto3 { get; private set; }
    }
}
