using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Celler.Dominio.Models;
using System.Collections;

namespace Celler.Infraestrutura.Repositorios
{
    public class AnuncioRepositorio
    {
        readonly Contexto _contexto;

        public AnuncioRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public Anuncio Obter(int id)
        {
            return _contexto.Anuncio.FirstOrDefault(a => a.Id == id);
        }

        public Anuncio ObterCompleto (int id)
        {
            return _contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .Include(a => a.Comentarios.Select(a1 => a1.Usuario))
                .FirstOrDefault(a => a.Id == id);
        }

        public List<AnuncioModelFeed> ObterUltimosAnuncios(int pagina)
        {
            //
            // Devido ao fato da classe abstrata não conter todo o necessário, a querry só retorna 
            // o que pode ser coletado genericamente
            //
            List<AnuncioModelFeed> anuncios = _contexto.Anuncio
                 .Include(a => a.Criador)
                 .Include(a => a.Comentarios)
                 .OrderByDescending(a => a.DataAnuncio)
                 .AsEnumerable()
                 .Select(a => new AnuncioModelFeed(a.Id,
                                                a.Titulo,
                                                a.Descricao,
                                                a.DataAnuncio,
                                                a.TipoAnuncio,
                                                a.Foto1,
                                                a.Foto2,
                                                a.Foto3,
                                                a.Criador.Nome,
                                                a.Status,
                                                a.Comentarios.Count))
                 //Status
                 .Where(a => a.Status != "d")
                 .Skip(pagina)
                 .Take(9)
                 .ToList();

            //
            // Para completar com as informações adicionais, é usado um método de preenchimento
            //
            PreencherInformacoesAdicionaisEspecificas(anuncios);

            return anuncios;
        }

        public object ObterUltimosAnuncios(int pagina, string filtro1, string filtro2, string filtro3, string search)
        {
            List<AnuncioModelFeed> anuncios = _contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .OrderByDescending(a => a.DataAnuncio)
                .ToList()
                .AsEnumerable()
                .Select(a => new AnuncioModelFeed(a.Id,
                                               a.Titulo,
                                               a.Descricao,
                                               a.DataAnuncio,
                                               a.TipoAnuncio,
                                               a.Foto1,
                                               a.Foto2,
                                               a.Foto3,
                                               a.Criador.Nome,
                                               a.Status,
                                               a.Comentarios.Count))
                //Status
                .Where(a => a.Status != "d")
                //Filtros
                .Where(a =>
                      (a.TipoAnuncio.ToUpper() == filtro1.ToUpper()) ||
                      (filtro2 != null ? a.TipoAnuncio.ToUpper() == filtro2.ToUpper() : false) ||
                      (filtro3 != null ? a.TipoAnuncio.ToUpper() == filtro3.ToUpper() : false))
                //Busca
                .Where(a => (search != null ?
                      a.Titulo.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                      a.Descricao.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                      : true))
                .Skip(pagina)
                .Take(9)
                .ToList();

            PreencherInformacoesAdicionaisEspecificas(anuncios);

            return anuncios;
        }

        public void Alterar(Anuncio anuncio)
        {
            _contexto.Entry(anuncio).State = EntityState.Modified;
        }

        private void PreencherInformacoesAdicionaisEspecificas(List<AnuncioModelFeed> anuncios)
        {
            foreach (AnuncioModelFeed anuncio in anuncios)
            {
                switch (anuncio.TipoAnuncio)
                {
                    case "Evento":
                        anuncio.NumeroInteressados = GetNumeroConfirmadosEventos(anuncio);
                        break;

                    case "Produto":
                        anuncio.NumeroInteressados = GetNumeroInteressadosProduto(anuncio);
                        anuncio.ValorProduto = GetValorProduto(anuncio);
                        break;

                    case "Vaquinha":
                        anuncio.NumeroInteressados = GetNumeroDoadoresVaquinha(anuncio);
                        break;

                    default: break;
                }
            }
        }

        private int GetNumeroConfirmadosEventos(AnuncioModelFeed anuncio)
        {
            return _contexto.Evento
                           .Include(a => a.Confirmados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Confirmados.Count;
        }

        private int GetNumeroInteressadosProduto(AnuncioModelFeed anuncio)
        {
            return _contexto.Produto
                           .Include(a => a.Interessados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Interessados.Count;
        }

        private int GetNumeroDoadoresVaquinha(AnuncioModelFeed anuncio)
        {
            return _contexto.Vaquinha
                           .Include(a => a.Doadores)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Doadores.Count;
        }

        private double GetValorProduto(AnuncioModelFeed anuncio)
        {
            return _contexto.Produto
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Valor;
        }

        public dynamic ObterComentariosPorId(int id, int pagina)
        {
            var result = _contexto.Anuncio
                .Where(a => a.Id == id)
                .SelectMany(a => a.Comentarios)
                .OrderByDescending(p => p.DataComentario)
                .Skip(pagina)
                .Take(3);

            return result;
        }

        public object ObterDetalhesAnuncio(Anuncio anuncio, bool usuarioLogado, ref ProdutoRepositorio _produtoRepositorio, ref EventoRepositorio _eventoRepositorio)
        {
            object retorno = null;
            switch (anuncio.TipoAnuncio)
            {
                case "Evento":
                    {
                        EventoModelDetalhes eventoModel = new EventoModelDetalhes(anuncio);
                        Evento evento = _eventoRepositorio.ObterPorId(anuncio.Id);
                        eventoModel.PopularComentarios(evento);
                        if (usuarioLogado)
                            eventoModel.PopularConfirmados(evento);
                        else
                            eventoModel.ContarConfirmados(evento);

                        retorno = eventoModel;
                        break;
                    }

                case "Produto":
                    {
                        ProdutoModelDetalhes produtoModel = new ProdutoModelDetalhes(anuncio);
                        Produto produto = _produtoRepositorio.ObterPorId(anuncio.Id);
                        produtoModel.PopularComentarios(produto);
                        if (usuarioLogado)
                            produtoModel.PopularConfirmados(produto);
                        else
                            produtoModel.ContarConfirmados(produto);

                        retorno = produtoModel;
                        break;
                    }

                /*case "Vaquinha":
                    {
                        VaquinhaModelDetalhes vaquinhaModel = new VaquinhaModelDetalhes(anuncio);
                        Vaquinha vaquinha = _produtoRepositorio.ObterPorId(anuncio.Id);
                        produtoModel.PopularComentarios(vaquinha);
                        if (usuarioLogado)
                            vaquinhaModel.PopularConfirmados(vaquinha);
                        else
                            vaquinhaModel.ContarConfirmados(vaquinha);

                        retorno = vaquinhaModel;
                        break;
                    }*/
            }

            return retorno;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
