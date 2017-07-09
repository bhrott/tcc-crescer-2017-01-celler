using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Celler.Dominio.Models
{
    public class AnuncioModel
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataAnuncio { get; private set; }
        public string TipoAnuncio { get; private set; }
        public string Foto1 { get; private set; }
        public string Foto2 { get; private set; }
        public string Foto3 { get; private set; }
        public string NomeCriador { get; private set; }
        public string Status { get; private set; }
        public int NumeroComentarios { get; private set; }
        public int NumeroInteressados { get; set; }
        

        public AnuncioModel(int id, string titulo, string descricao, DateTime dataAnuncio, string tipoAnuncio, string foto1, string foto2, string foto3, string nomeCriador, string status, int numeroComentarios, int numeroInteressados)
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
            NumeroInteressados = numeroInteressados;
        }
    }
}