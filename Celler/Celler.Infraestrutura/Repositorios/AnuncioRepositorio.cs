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

        public List<AnuncioModel> ObterUltimosAnuncios(int pagina)
        {
            //
            // Devido ao fato da classe abstrata não conter todo o necessário, a querry só retorna 
            // o que pode ser coletado genericamente
            //
            List<AnuncioModel> anuncios = _contexto.Anuncio
                 .Include(a => a.Criador)
                 .Include(a => a.Comentarios)
                 .OrderByDescending(a => a.DataAnuncio)
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
            List<AnuncioModel> anuncios = _contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .OrderByDescending(a => a.DataAnuncio)
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

        public void ComentarAnuncio(Anuncio anuncio, Comentario comentario)
        {
            anuncio.AdicionarComentario(comentario);
            _contexto.Entry(anuncio).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        private void PreencherInformacoesAdicionaisEspecificas(List<AnuncioModel> anuncios)
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
                        anuncio.ValorProduto = GetValorProduto(anuncio);
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
            return _contexto.Evento
                           .Include(a => a.Confirmados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Confirmados.Count;
        }

        private int GetNumeroInteressadosProduto(AnuncioModel anuncio)
        {
            return _contexto.Produto
                           .Include(a => a.Interessados)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Interessados.Count;
        }

        private int GetNumeroDoadoresVaquinha(AnuncioModel anuncio)
        {
            return _contexto.Vaquinha
                           .Include(a => a.Doadores)
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Doadores.Count;
        }

        private double GetValorProduto(AnuncioModel anuncio)
        {
            return _contexto.Produto
                           .SingleOrDefault(a => a.Id == anuncio.Id)
                           .Valor;
        }


        public IEnumerable ObterAnuncioPorId(int id)
        {
            Anuncio anuncio = _contexto.Anuncio
                .Include(a => a.Criador)
                .Include(a => a.Comentarios)
                .Include(a => a.Comentarios.Select(a1 => a1.Usuario))
                .FirstOrDefault(a => a.Id == id);

            var AnuncioDetalhado = PreencherAnuncioDetalhado(anuncio);

            return AnuncioDetalhado;

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
            AnuncioDetalhado.IdUsuario = anuncio.Criador.Id;
            AnuncioDetalhado.Comentarios = new List<IEnumerable>();
            AnuncioDetalhado.Status = anuncio.Status;

            foreach (var comentarioAnuncio in anuncio.Comentarios)
            {
                dynamic Comentario = new System.Dynamic.ExpandoObject();
                Comentario.Texto = comentarioAnuncio.Texto;
                Comentario.Id = comentarioAnuncio.Id;
                Comentario.DataComentario = comentarioAnuncio.DataComentario;
                Comentario.UsuarioNome = comentarioAnuncio.Usuario.Nome;
                Comentario.UsuarioEmail = comentarioAnuncio.Usuario.Email;
                Comentario.UsuarioId = comentarioAnuncio.Usuario.Id;
                AnuncioDetalhado.Comentarios.Add(Comentario);
            }

            switch (anuncio.TipoAnuncio)
            {
                case "Evento":
                    Evento evento = _contexto.Evento
                        .Include(e => e.Confirmados)
                        .FirstOrDefault(e => e.Id == anuncio.Id);
                    AnuncioDetalhado.DataRealizacao = evento.DataRealizacao;
                    AnuncioDetalhado.Local = evento.Local;
                    AnuncioDetalhado.DataMaximaConfirmacao = evento.DataMaximaConfirmacao;
                    AnuncioDetalhado.ValorPorPessoa = evento.ValorPorPessoa;
                    AnuncioDetalhado.Confirmados = new List<IEnumerable>();

                    foreach (var interessadoEvento in evento.Confirmados)
                    {
                        dynamic Interessado = new System.Dynamic.ExpandoObject();
                        Interessado.Id = interessadoEvento.Id;
                        Interessado.Nome = interessadoEvento.Nome;
                        Interessado.Email = interessadoEvento.Email;
                        AnuncioDetalhado.Confirmados.Add(Interessado);
                    }
                    return AnuncioDetalhado;

                case "Produto":
                    Produto produto = _contexto.Produto
                        .Include(p => p.Interessados)
                        .FirstOrDefault(p => p.Id == anuncio.Id);

                    AnuncioDetalhado.Valor = produto.Valor;
                    AnuncioDetalhado.Interessados = new List<IEnumerable>();

                    foreach (var interessadoProduto in produto.Interessados)
                    {
                        dynamic Interessado = new System.Dynamic.ExpandoObject();
                        Interessado.Id = interessadoProduto.Id;
                        Interessado.Nome = interessadoProduto.Nome;
                        Interessado.Email = interessadoProduto.Email;
                        AnuncioDetalhado.Interessados.Add(Interessado);
                    }

                    return AnuncioDetalhado;

                case "Vaquinha":
                    Vaquinha vaquinha = _contexto.Vaquinha
                        .Include(v => v.Doadores)
                        .Include(v => v.Doadores.Select(v1 => v1.Usuario))
                        .FirstOrDefault(v => v.Id == anuncio.Id);
                    AnuncioDetalhado.ArrecadamentoPrevisto = vaquinha.ArrecadamentoPrevisto;
                    AnuncioDetalhado.TotalArrecadado = vaquinha.TotalArrecadado;
                    AnuncioDetalhado.DateTermino = vaquinha.DateTermino;
                    AnuncioDetalhado.Doadores = new List<IEnumerable>();
                    foreach (var interessadoVaquinha in vaquinha.Doadores)
                    {
                        dynamic Interessado = new System.Dynamic.ExpandoObject();
                        Interessado.Id = interessadoVaquinha.Usuario.Id;
                        Interessado.Nome = interessadoVaquinha.Usuario.Nome;
                        Interessado.Email = interessadoVaquinha.Usuario.Email;
                        Interessado.Valor = interessadoVaquinha.ValorDoado;
                        Interessado.Status = interessadoVaquinha.Status;
                        AnuncioDetalhado.Doadores.Add(Interessado);
                    }

                    return AnuncioDetalhado;

                default: return null;
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
