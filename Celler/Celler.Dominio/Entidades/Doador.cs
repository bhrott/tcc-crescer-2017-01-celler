﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Doador
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public double ValorDoado { get; private set; }
        //Status: 'p' - pago; 'n' - não pago
        public char Status { get; private set; }

        public Doador(){}
    }
}
