using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Repositorios
{
    public class ProdutoRepositorio
    {
        readonly Contexto _contexto;

        public ProdutoRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public Produto ObterPorId(int id)
        {
            return _contexto.Produto
                .Include(p => p.Interessados)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Alterar(Produto produto)
        {
            _contexto.Entry(produto).State = EntityState.Modified;
        }
    }
}
