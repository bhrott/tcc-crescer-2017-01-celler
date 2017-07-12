using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
    public class DoarVaquinhaModel
    {
        public int IdUsuario { get; set; }

        public int IdVaquinha { get; set; }

        public double ValorDoado { get; set; }
    }
}