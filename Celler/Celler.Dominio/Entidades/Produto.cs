using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Entidades
{
    public class Produto : Anuncio
    {
        public double Valor { get; private set; }
        public Usuario Comprador { get; private set; }
        public List<Usuario> Interessados { get; set; }

        protected Produto(){ }

        public override int GetNumeroPessoasComInteresse()
        {
            return Interessados.Count;
        }

        public void AdicionarInteressado (Usuario usuario)
        {
            Interessados.Add(usuario);
        }
    }
}
