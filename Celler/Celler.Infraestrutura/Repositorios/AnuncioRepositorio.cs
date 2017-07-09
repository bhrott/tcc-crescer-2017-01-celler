using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;
using Celler.Dominio.Models;

namespace Celler.Infraestrutura.Repositorios
{
    public class AnuncioRepositorio
    {
        private Contexto contexto = new Contexto();

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
                .Skip(pagina)
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
                                               a.Comentarios.Count,
                                               0))
                .ToList();

            //
            // Para completar com as informações adicionais, é usado um método de preenchimento
            //
            PreencherNumeroDeInteressados(anuncios);

            return anuncios;
        }

        public object ObterUltimosAnuncios(string[] filtros)
        {
            //
            // Devido ao fato de arrays e listas não poderem ser usadas dentro de expressões LINQ, variáveis temporárias foram criadas.
            //
            string tmpFiltro0 = filtros[0];
            string tmpFiltro1 = filtros[1];
            string tmpFiltro2 = filtros[2];

            List<AnuncioModel> anuncios = contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .OrderByDescending(a => a.DataAnuncio)
                .Take(9)
                .ToList()
                .AsEnumerable()
                .Select(a => new AnuncioModel(a.Id,
                                               a.Titulo,
                                               a.Descricao,
                                               a.DataAnuncio,
                                               a.TipoAnuncio,
                                               a.Foto1,
                                               a.Foto2,
                                               a.Foto3,
                                               a.Criador.Nome,
                                               a.Comentarios.Count,
                                               0))

                .Where(a=> 
                      (a.TipoAnuncio.ToUpper() == tmpFiltro0.ToUpper()) ||
                      (tmpFiltro1 != null ? a.TipoAnuncio.ToUpper() == tmpFiltro1.ToUpper() : false) ||
                      (tmpFiltro2 != null ? a.TipoAnuncio.ToUpper() == tmpFiltro2.ToUpper() : false))
                .ToList();

            PreencherNumeroDeInteressados(anuncios);

            return anuncios;
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
    }
}
