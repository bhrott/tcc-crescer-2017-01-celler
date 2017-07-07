using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public abstract class Anuncio
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Foto1 { get; private set; }
        public string Foto2 { get; private set; }
        public string Foto3 { get; private set; }
        public DateTime DataAnuncio { get; set; }
        public Usuario Criador { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public Anuncio() { }
    }
}
