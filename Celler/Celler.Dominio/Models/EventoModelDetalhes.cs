using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class EventoModelDetalhes : AnuncioModelDetalhes
    {
        public DateTime DataRealizacao { get; set; }
        public string Local { get; set; }
        public DateTime DataMaximaConfirmacao { get; set; }
        public double ValorPorPessoa { get; set; }
        public List<UsuarioModel> Confirmados { get; set; }
        public int NumeroConfirmados { get; set; }
    }
}
