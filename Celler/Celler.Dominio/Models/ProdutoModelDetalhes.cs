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
        public int NumeroInteressados { get; set; }

        public ProdutoModelDetalhes(int id,
                                   string titulo,
                                   string descricao,
                                   DateTime dataAnuncio,
                                   string tipoAnuncio,
                                   string foto1,
                                   string foto2,
                                   string foto3,
                                   UsuarioModel criador,
                                   string status)
                                   :base (id,
                                          titulo,
                                          descricao,
                                          dataAnuncio,
                                          tipoAnuncio,
                                          foto1,
                                          foto2,
                                          foto3,
                                          criador,
                                          status)
        { }
    }
}
