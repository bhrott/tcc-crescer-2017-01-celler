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
                                               a.Status,
                                               a.Comentarios.Count,
                                               0))
                //Status
                .Where(a => a.Status != "d")
                .ToList();

            //
            // Para completar com as informações adicionais, é usado um método de preenchimento
            //
            PreencherNumeroDeInteressados(anuncios);

            return anuncios;
        }

        public object ObterUltimosAnuncios(string filtro1, string filtro2, string filtro3, string search)
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

        public IEnumerable ObterAnuncioPorId(int id)
        {
            Anuncio anuncio = contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .FirstOrDefault(a => a.Id == id);

            var AnuncioDetalhado = PreencherAnuncioDetalhado(anuncio);

            return AnuncioDetalhado;

        }

        private IEnumerable PreencherAnuncioDetalhado(Anuncio anuncio)
        {
            dynamic AnuncioDetalhado = new System.Dynamic.ExpandoObject();
            AnuncioDetalhado.Id = anuncio.Id;
            AnuncioDetalhado.Titulo = anuncio.Titulo;
            AnuncioDetalhado.Descricao = anuncio.Descricao;
            AnuncioDetalhado.DataAnuncio = anuncio.DataAnuncio;
            AnuncioDetalhado.TipoAnuncio = anuncio.TipoAnuncio;
            AnuncioDetalhado.Foto1 = anuncio.Foto1;
            AnuncioDetalhado.Foto2 = anuncio.Foto2;
            AnuncioDetalhado.Foto3 = anuncio.Foto3;
            AnuncioDetalhado.NomeCriador = anuncio.Criador.Nome;
            AnuncioDetalhado.Comentarios = anuncio.Comentarios;

            switch (anuncio.TipoAnuncio)
            {
                case "Evento":
                    Evento evento = contexto.Evento.FirstOrDefault(e => e.Id == anuncio.Id);
                    AnuncioDetalhado.DataRealizacao = evento.DataRealizacao;
                    AnuncioDetalhado.Local = evento.Local;
                    AnuncioDetalhado.DataMaximaConfirmacao = evento.DataMaximaConfirmacao;
                    AnuncioDetalhado.ValorPorPessoa = evento.ValorPorPessoa;
                    AnuncioDetalhado.Confirmados = evento.Confirmados;
                    return AnuncioDetalhado;

                case "Produto":
                    Produto produto = contexto.Produto.FirstOrDefault(p => p.Id == anuncio.Id);
                    AnuncioDetalhado.Valor = produto.Valor;
                    AnuncioDetalhado.Comprador = produto.Comprador;
                    AnuncioDetalhado.Interessados = produto.Interessados;

                    return AnuncioDetalhado;

                case "Vaquinha":
                    Vaquinha vaquinha = contexto.Vaquinha.FirstOrDefault(v => v.Id == anuncio.Id);
                    AnuncioDetalhado.ArrecadamentoPrevisto = vaquinha.ArrecadamentoPrevisto;
                    AnuncioDetalhado.TotalArrecadado = vaquinha.TotalArrecadado;
                    AnuncioDetalhado.DateTermino = vaquinha.DateTermino;
                    AnuncioDetalhado.Doadores = vaquinha.Doadores;
                    return AnuncioDetalhado;

                default: return null;
            }

        }
    }
}
