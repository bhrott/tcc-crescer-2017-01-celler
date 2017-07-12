using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class ComentarioModelDetalhes
    {
        public ComentarioModelDetalhes(int id, string texto, UsuarioModel usuario, DateTime dataComentario)
        {
            Id = id;
            Texto = texto;
            Usuario = usuario;
            DataComentario = dataComentario;
        }

        public int Id { get; private set; }
        public string Texto { get; private set; }
        public UsuarioModel Usuario { get; private set; }
        public DateTime DataComentario { get; private set; }
    }
}
