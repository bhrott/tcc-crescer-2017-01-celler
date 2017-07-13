﻿using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
        public class SalvarProdutoModel
        {
            public string Titulo { get; set; }
            public string Descricao { get; set; }
            public string Foto1 { get; set; }
            public string Foto2 { get; set; }
            public string Foto3 { get; set; }
            public double Valor { get; set; }
        }
}