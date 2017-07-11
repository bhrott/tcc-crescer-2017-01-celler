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
    }
}
