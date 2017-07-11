using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class VaquinhaModelDetalhes : AnuncioModelDetalhes
    {
        public double ArrecadamentoPrevisto { get; set; }
        public double TotalArrecadado { get; set; }
        public DateTime DataTermino { get; set; }
        public List<DoadorModel> Doadores { get; set; }
        public int NumeroDoadores { get; set; }

        public VaquinhaModelDetalhes(int id,
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
