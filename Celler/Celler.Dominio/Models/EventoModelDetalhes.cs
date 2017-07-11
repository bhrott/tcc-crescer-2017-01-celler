using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class EventoModelDetalhes : AnuncioModelDetalhes
    {
        public DateTime DataRealizacao { get; set; }
        public string Local { get; set; }
        public DateTime DataMaximaConfirmacao { get; set; }
        public double ValorPorPessoa { get; set; }
        public List<UsuarioModel> Confirmados { get; set; }
        public int NumeroConfirmados { get; set; }

        public EventoModelDetalhes(int id, 
                                   string titulo, 
                                   string descricao, 
                                   DateTime dataAnuncio, 
                                   string tipoAnuncio, 
                                   string foto1, 
                                   string foto2, 
                                   string foto3, 
                                   UsuarioModel criador, 
                                   string status)
                                   :base (id,
                                          titulo,
                                          descricao,
                                          dataAnuncio,
                                          tipoAnuncio,
                                          foto1,
                                          foto2,
                                          foto3,
                                          criador,
                                          status)
        { }

        public EventoModelDetalhes(Anuncio anuncio)
                                   : base(anuncio.Id,
                                          anuncio.Titulo,
                                          anuncio.Descricao,
                                          anuncio.DataAnuncio,
                                          anuncio.TipoAnuncio,
                                          anuncio.Foto1,
                                          anuncio.Foto2,
                                          anuncio.Foto3,
                                          new UsuarioModel (anuncio.Criador.Id, anuncio.Criador.Nome, anuncio.Criador.Email),
                                          anuncio.Status)
        { }

        public void PopularComentarios(Anuncio anuncio)
        {
            Comentarios = new List<ComentarioModel>();

            foreach (var comentarioAnuncio in anuncio.Comentarios)
            {
                UsuarioModel usuarioComentador = new UsuarioModel(comentarioAnuncio.Usuario.Id,
                                                                  comentarioAnuncio.Usuario.Nome,
                                                                  comentarioAnuncio.Usuario.Email);

                Comentarios.Add(new ComentarioModel(comentarioAnuncio.Id,
                                                    comentarioAnuncio.Texto,
                                                    usuarioComentador,
                                                    comentarioAnuncio.DataComentario));
            }
        }

        public void PopularConfirmados(Anuncio anuncio)
        {
            Confirmados = new List<UsuarioModel>();
            Evento evento = (Evento)anuncio;

            foreach (var confirmados in evento.Confirmados)
            {
                this.Confirmados.Add (new UsuarioModel(confirmados.Id,
                                                       confirmados.Nome,
                                                       confirmados.Email));
            }
            ContarConfirmados(anuncio);
        }

        public void ContarConfirmados(Anuncio anuncio)
        {
            Confirmados = new List<UsuarioModel>();
            Evento evento = (Evento)anuncio;
            this.NumeroConfirmados = evento.Confirmados.Count;
        }
    }
}
