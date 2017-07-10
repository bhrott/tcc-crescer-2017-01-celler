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

        public void SalvarInteressadoProduto(int idUsuario, int idProduto)
        {
            Produto produto = _contexto.Produto
                .Include(p => p.Interessados)
                .FirstOrDefault(p => p.Id == idProduto);
            Usuario usuario = _contexto.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            produto.AdicionarInteressado(usuario);
            _contexto.Entry(produto).State = EntityState.Modified;
            _contexto.SaveChanges();

        }
    }
}
