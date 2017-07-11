using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public abstract class Anuncio : EntidadeBasica
    {
        public int Id { get; protected set; }
        public string Titulo { get; protected set; }
        public string Descricao { get; protected set; }
        public string Foto1 { get; protected set; }
        public string Foto2 { get; protected set; }
        public string Foto3 { get; protected set; }
        public DateTime DataAnuncio { get; protected set; }
        public Usuario Criador { get; protected set; }
        public List<Comentario> Comentarios { get; protected set; }
        public string TipoAnuncio { get; protected set; }
        /// <summary>
        /// 'f' - fechado(vendido/realizado); 'a' - anunciado; 'd' - deletado 
        /// </summary>
        public string Status { get; protected set; }

        protected Anuncio() { }

        protected Anuncio(string titulo,
            string descricao,
            Usuario criador,
            string tipoAnuncio)
        {
            Comentarios = new List<Comentario>();
            Titulo = titulo;
            Descricao = descricao;
            DataAnuncio = DateTime.Now;
            Criador = criador;
            TipoAnuncio = tipoAnuncio;
            Status = "A";

            if (string.IsNullOrWhiteSpace(Titulo))
                Mensagens.Add("Informe um título.");

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Informe uma descrição.");

            if (criador == null)
                Mensagens.Add("Informe o criador.");

            if (string.IsNullOrWhiteSpace(TipoAnuncio))
                Mensagens.Add("Informe o tipo do anuncio.");
        }

        public void AdicionarComentario(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }

        public abstract int GetNumeroPessoasComInteresse();
    }
}
