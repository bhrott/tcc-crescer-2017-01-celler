using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Celler.Dominio.Models
{
    public class AnuncioModelFeed
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAnuncio { get; set; }
        public string TipoAnuncio { get; set; }
        public string Foto1 { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public string NomeCriador { get; set; }
        public string Status { get; set; }
        public int NumeroComentarios { get; set; }
        public int NumeroInteressados { get; set; }
        public double ValorProduto { get; set; }
        public bool TemInteresse { get; set; }


        public AnuncioModelFeed(int id, string titulo, string descricao, DateTime dataAnuncio, string tipoAnuncio, string foto1, string foto2, string foto3, string nomeCriador, string status, int numeroComentarios)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            DataAnuncio = dataAnuncio;
            TipoAnuncio = tipoAnuncio;
            Foto1 = foto1;
            Foto2 = foto2;
            Foto3 = foto3;
            NomeCriador = nomeCriador;
            Status = status;
            NumeroComentarios = numeroComentarios;
        }
    }
}