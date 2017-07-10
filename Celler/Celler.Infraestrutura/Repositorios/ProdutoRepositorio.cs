using System;
using System.Collections.Generic;
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
            //Não implementado
        }
    }
}
