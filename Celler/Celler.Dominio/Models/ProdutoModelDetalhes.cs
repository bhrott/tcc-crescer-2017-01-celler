using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class ProdutoModelDetalhes : AnuncioModelDetalhes
    {
        public double Valor { get; set; }
        public UsuarioModel Comprador { get; set; }
        public List<UsuarioModel> Interessados { get; set; }
    }
}
