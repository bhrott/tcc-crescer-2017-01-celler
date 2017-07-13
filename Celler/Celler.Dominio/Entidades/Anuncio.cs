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

        public static readonly string Erro_Sem_Titulo = "Informe um título.";
        public static readonly string Erro_Sem_Descricao = "Informe uma descrição.";
        public static readonly string Erro_Sem_Criador = "Informe o criador.";
        public static readonly string Erro_Tipo_Anuncio = "Informe o tipo do anuncio.";

        protected Anuncio() { }

        protected Anuncio(string titulo,
            string descricao,
            Usuario criador,
            string tipoAnuncio,
            string foto1,
            string foto2,
            string foto3)
        {
            Foto1 = foto1;
            Foto2 = foto2;
            Foto3 = foto3;
            Comentarios = new List<Comentario>();
            Titulo = titulo;
            Descricao = descricao;
            DataAnuncio = DateTime.Now;
            Criador = criador;
            TipoAnuncio = tipoAnuncio;
            Status = "A";

            if (string.IsNullOrWhiteSpace(Titulo))
                Mensagens.Add(Erro_Sem_Titulo);

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add(Erro_Sem_Descricao);

            if (criador == null)
                Mensagens.Add(Erro_Sem_Criador);

            if (string.IsNullOrWhiteSpace(TipoAnuncio))
                Mensagens.Add(Erro_Tipo_Anuncio);
        }

        public void AdicionarComentario(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }

        public abstract int GetNumeroPessoasComInteresse();
    }
}
