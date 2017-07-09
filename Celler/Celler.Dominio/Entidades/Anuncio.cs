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
        public DateTime DataAnuncio { get; private set; }
        public Usuario Criador { get; private set; }
        public List<Comentario> Comentarios { get; private set; }
        public string TipoAnuncio { get; private set; }

        public Anuncio() { }

        public abstract int GetNumeroPessoasComInteresse();
    }
}
