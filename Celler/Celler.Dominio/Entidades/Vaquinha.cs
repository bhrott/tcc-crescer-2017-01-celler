﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Vaquinha : Anuncio
    {
        public double ArrecadamentoPrevisto { get; private set; }
        //Status: 'a' - anunciada;'e' - encerrada; 'd' - deletada; 
        public string Status { get; private set; }
        public double TotalArrecadado { get; private set; }
        public DateTime DateTermino { get; private set; }
        public List<Doador> Doadores { get; set; }

        public Vaquinha() { }
    }
}
