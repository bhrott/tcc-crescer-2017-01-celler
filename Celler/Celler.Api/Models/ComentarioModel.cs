using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
    public class ComentarioModel
    {
        public string Texto { get; set; }
        public int IdAnuncio { get; set; }

    }
}