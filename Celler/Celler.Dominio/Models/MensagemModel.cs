using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class MensagemModel
    {

        public string Assunto { get; set; }
        public string Texto { get; set; }
        public MensagemModel(string assunto, string texto)
        {
            this.Assunto = assunto;
            this.Texto = texto;
        }
    }
}
