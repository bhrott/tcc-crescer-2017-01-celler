
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Vaquinha
    {
        public int Id { get; private set; }

        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public double ArrecadamentoPrevisto { get; private set; }

        public double TotalArrecadado { get; private set; }

        public DateTime DataAbertura { get; private set; }

        public DateTime DateTermino { get; private set; }

        public Usuario Criador { get; private set; }

        public string Foto1 { get; private set; }

        public string Foto2 { get; private set; }

        public string Foto3 { get; private set; }

        public List<Comentario> Comentarios { get; private set; }

        public List<Doador> Doadores { get; private set; }

        public Vaquinha() { }
    }
}
