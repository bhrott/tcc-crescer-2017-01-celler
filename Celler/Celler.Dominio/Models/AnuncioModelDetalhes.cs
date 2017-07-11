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
        public AnuncioModelDetalhes(int id, string titulo, string descricao, DateTime dataAnuncio, string tipoAnuncio, string foto1, string foto2, string foto3, UsuarioModel criador, string status)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Foto1 = foto1;
            Foto2 = foto2;
            Foto3 = foto3;
            DataAnuncio = dataAnuncio;
            Criador = criador;
            TipoAnuncio = tipoAnuncio;
            Status = status;

            Comentarios = new List<ComentarioModel>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAnuncio { get; set; }
        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public UsuarioModel Criador { get; set; }
        public List<ComentarioModel> Comentarios { get; set; }
        public string TipoAnuncio { get; set; }
        public string Status { get; set; }
    }
}
