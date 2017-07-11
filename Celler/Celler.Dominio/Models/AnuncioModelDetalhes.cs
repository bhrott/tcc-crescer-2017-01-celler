using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public abstract class AnuncioModelDetalhes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public DateTime DataAnuncio { get; set; }
        public UsuarioModel Criador { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public string TipoAnuncio { get; set; }
    }
}
