using Celler.Dominio.Entidades;
using Celler.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public List<AnuncioModelFeed> ObterAnunciosUsuario(int pagina, Usuario usuarioLogado)
        {
            //
            // Devido ao fato da classe abstrata não conter todo o necessário, a querry só retorna 
            // o que pode ser coletado genericamente
            //
            List<AnuncioModelFeed> anuncios = _contexto.Anuncio
                 .Include(a => a.Criador)
                 .Include(a => a.Comentarios)
                 .OrderByDescending(a => a.DataAnuncio)
                 .Where(x => x.Criador.Id == usuarioLogado.Id)
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
            PreencherInformacoesAdicionaisEspecificas(anuncios, usuarioLogado);

            return anuncios;
        }

        public dynamic ObterUltimosAnuncios(int pagina, string filtro1, string filtro2, string filtro3, string search, Usuario usuarioLogado)
        {
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
                .Where(a => a.Status == "a")
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

            PreencherInformacoesAdicionaisEspecificas(anuncios, usuarioLogado);

            return anuncios;
        }

        public void Alterar(Anuncio anuncio)
        {
            _contexto.Entry(anuncio).State = EntityState.Modified;
        }

        private void PreencherInformacoesAdicionaisEspecificas(List<AnuncioModelFeed> anuncios, Usuario usuarioLogado)
        {
            foreach (AnuncioModelFeed anuncio in anuncios)
            {
                switch (anuncio.TipoAnuncio)
                {
                    case TipoAnuncio.EVENTO:
                        anuncio.NumeroInteressados = GetNumeroConfirmadosEventos(anuncio);
                        anuncio.TemInteresse = UsuarioLogadoConfirmado(anuncio, usuarioLogado);
                        anuncio.Postou = UsuarioLogadoPostou(anuncio, usuarioLogado);
                        break;

                    case TipoAnuncio.PRODUTO:
                        anuncio.NumeroInteressados = GetNumeroInteressadosProduto(anuncio);
                        anuncio.ValorProduto = GetValorProduto(anuncio);
                        anuncio.TemInteresse = UsuarioLogadoInteressado(anuncio, usuarioLogado);
                        anuncio.Postou = UsuarioLogadoPostou(anuncio, usuarioLogado);
                        break;

                    case TipoAnuncio.VAQUINHA:
                        anuncio.NumeroInteressados = GetNumeroDoadoresVaquinha(anuncio);
                        anuncio.TemInteresse = UsuarioLogadoDoou(anuncio, usuarioLogado);
                        anuncio.Postou = UsuarioLogadoPostou(anuncio, usuarioLogado);
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

        private bool UsuarioLogadoConfirmado(AnuncioModelFeed anuncio, Usuario usuario)
        {
            return _contexto.Evento
                           .Include(a => a.Confirmados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Confirmados
                           .AsEnumerable()
                           .Select (e => e.Id == usuario.Id)
                           .Count() > 0;
        }

        private bool UsuarioLogadoInteressado(AnuncioModelFeed anuncio, Usuario usuario)
        {
            return _contexto.Produto
                           .Include(a => a.Interessados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Interessados
                           .AsEnumerable()
                           .Select(e => e.Id == usuario.Id)
                           .Count() > 0;
        }

        private bool UsuarioLogadoDoou(AnuncioModelFeed anuncio, Usuario usuario)
        {
            return _contexto.Vaquinha
                           .Include(a => a.Doadores)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Doadores
                           .AsEnumerable()
                           .Select(e => e.Id == usuario.Id)
                           .Count() > 0;
        }

        private bool UsuarioLogadoPostou(AnuncioModelFeed anuncio, Usuario usuario)
        {
            return _contexto.Anuncio
                           .Include(a => a.Criador)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Criador.Id == usuario.Id;
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

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
