using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
    public class ComentarioModel : EntidadeBasica
    {
        public string  Texto { get; set; }
        public int IdAnuncio { get; set; }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Texto))
                Mensagens.Add("O texto informado não é válido.");

            return Mensagens.Count == 0;
        }
    }
}