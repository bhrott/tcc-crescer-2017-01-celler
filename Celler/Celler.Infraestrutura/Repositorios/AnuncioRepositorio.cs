using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Celler.Infraestrutura.Repositorios
{
    public class AnuncioRepositorio
    {
        private Contexto contexto = new Contexto();

        public IQueryable<Anuncio> ObterUltimosAnuncios(int pagina)
        {
            return contexto.Anuncio.Include(x => x.Criador)
                .OrderByDescending(a => a.DataAnuncio).Skip(pagina).Take(9);
        }
    }
}
