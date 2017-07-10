using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Celler.Dominio.Models;

namespace Celler.Infraestrutura.Repositorios
{
    public class AnuncioRepositorio
    {
        private Contexto contexto = new Contexto();

        public Anuncio Obter (int id)
        {
            return contexto.Anuncio.FirstOrDefault(a => a.Id == id);
        }

        public List<AnuncioModel> ObterUltimosAnuncios(int pagina)
        {
            //
            // Devido ao fato da classe abstrata não conter todo o necessário, a querry só retorna 
            // o que pode ser coletado genericamente
            //
           List<AnuncioModel> anuncios = contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .OrderByDescending(a => a.DataAnuncio)
                .AsEnumerable()
                .Select(a => new AnuncioModel( a.Id,
                                               a.Titulo,
                                               a.Descricao,
                                               a.DataAnuncio,
                                               a.TipoAnuncio,
                                               a.Foto1,
                                               a.Foto2,
                                               a.Foto3,
                                               a.Criador.Nome,
                                               a.Status,
                                               a.Comentarios.Count,
                                               0))
                //Status
                .Where(a => a.Status != "d")
                .Skip(pagina)
                .Take(9)
                .ToList();

            //
            // Para completar com as informações adicionais, é usado um método de preenchimento
            //
            PreencherNumeroDeInteressados(anuncios);

            return anuncios;
        }

        public object ObterUltimosAnuncios(int pagina, string filtro1, string filtro2, string filtro3, string search)
        {
            List<AnuncioModel> anuncios = contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .OrderByDescending(a => a.DataAnuncio)
                .Take(9)
                .ToList()
                .AsEnumerable()
                .Select(a => new AnuncioModel( a.Id,
                                               a.Titulo,
                                               a.Descricao,
                                               a.DataAnuncio,
                                               a.TipoAnuncio,
                                               a.Foto1,
                                               a.Foto2,
                                               a.Foto3,
                                               a.Criador.Nome,
                                               a.Status,
                                               a.Comentarios.Count,
                                               0))
                //Status
                .Where(a => a.Status != "d")
                //Filtros
                .Where(a=> 
                      (a.TipoAnuncio.ToUpper() == filtro1.ToUpper()) ||
                      (filtro2 != null ? a.TipoAnuncio.ToUpper() == filtro2.ToUpper() : false) ||
                      (filtro3 != null ? a.TipoAnuncio.ToUpper() == filtro3.ToUpper() : false))
                //Busca
                .Where (a => (search != null ?
                       a.Titulo.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                       a.Descricao.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                       : true))
                .Skip(pagina)
                .Take(9)
                .ToList();

            PreencherNumeroDeInteressados(anuncios);

            return anuncios;
        }

        public bool ComentarAnuncio (string texto, int id, Usuario usuario)
        {
            Comentario comentario = new Comentario(texto, usuario, DateTime.Now);
            Anuncio anuncio = contexto.Anuncio.FirstOrDefault(a => a.Id == id);
            anuncio.AdicionarComentario(comentario);
            contexto.Entry(anuncio).State = EntityState.Modified;
            contexto.SaveChanges();
            return true;
        }

        private void PreencherNumeroDeInteressados(List<AnuncioModel> anuncios)
        {
            foreach (AnuncioModel anuncio in anuncios)
            {
                switch (anuncio.TipoAnuncio)
                {
                    case "Evento":
                        anuncio.NumeroInteressados = GetNumeroConfirmadosEventos(anuncio);
                        break;

                    case "Produto":
                        anuncio.NumeroInteressados = GetNumeroInteressadosProduto(anuncio);
                        break;

                    case "Vaquinha":
                        anuncio.NumeroInteressados = GetNumeroDoadoresVaquinha(anuncio);
                        break;

                    default: break;
                }
            }
        }

        private int GetNumeroConfirmadosEventos(AnuncioModel anuncio)
        {
            return contexto.Evento
                           .Include(a => a.Confirmados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Confirmados.Count;
        }

        private int GetNumeroInteressadosProduto(AnuncioModel anuncio)
        {
            return contexto.Produto
                           .Include(a => a.Interessados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Interessados.Count;
        }

        private int GetNumeroDoadoresVaquinha (AnuncioModel anuncio)
        {
            return contexto.Vaquinha
                           .Include(a => a.Doadores)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Doadores.Count;
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
