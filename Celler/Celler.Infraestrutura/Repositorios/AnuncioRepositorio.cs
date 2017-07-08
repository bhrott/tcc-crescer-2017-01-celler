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

namespace Celler.Infraestrutura.Repositorios
{
    public class AnuncioRepositorio
    {
        private Contexto contexto = new Contexto();
        private List<IEnumerable> devolver;

        public IEnumerable ObterUltimosAnuncios(int pagina)
        {
            var lista = contexto.Anuncio.Include(a => a.Criador)
                .OrderByDescending(a => a.DataAnuncio).Skip(pagina).Take(9);
            dynamic AnuncioResumido;

            foreach (var l in lista)
            {
                if (l.TipoAnuncio.Equals("Produto"))
                {
                    Produto produto = (Produto)contexto.Produto.Where(p => p.Id == l.Id);
                    AnuncioResumido = new
                    {
                        Id = l.Id,
                        Titulo = l.Titulo,
                        Foto1 = l.Foto1,
                        DataAnuncio = l.DataAnuncio,
                        Criador = l.Criador,
                        QuantidadeComentarios = l.Comentarios.Count(),
                        TipoAnuncio = l.TipoAnuncio,
                        ValorProduto = produto.Valor,
                        Status = produto.Status,
                        QuantidadeInteressados = produto.Interessados.Count()
                    };
                    devolver.Add(AnuncioResumido);
                }
                else if (l.TipoAnuncio.Equals("Evento"))
                {
                    Evento evento = (Evento)contexto.Evento.Where(e => e.Id == l.Id);
                    AnuncioResumido = new
                    {
                        Id = l.Id,
                        Titulo = l.Titulo,
                        Foto1 = l.Foto1,
                        DataAnuncio = l.DataAnuncio,
                        Criador = l.Criador,
                        QuantidadeComentarios = l.Comentarios.Count(),
                        QuantidadeConfirmados = evento.Confirmados.Count(),
                        TipoAnuncio = l.TipoAnuncio
                    };
                    devolver.Add(AnuncioResumido);
                }
                else
                {
                    devolver.Add(lista);
                }
            }
            return devolver;
        }
    }
}
