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

        protected Produto() { }

        public Produto(string titulo,
            string descricao,
            Usuario criador,
            double valor) : base(titulo, descricao, criador, "Produto")
        {
            Valor = valor;

            if (valor < 5)
                AdicionarMensagem("Produto com valor inferior a R$5.00.");
        }

        public override int GetNumeroPessoasComInteresse()
        {
            return Interessados.Count;
        }

        public void AdicionarInteressado(Usuario usuario)
        {
            if (Interessados.Contains(usuario))
                AdicionarMensagem("Usuário já está interessado neste produto.");
            else
                Interessados.Add(usuario);

        }

        public void RemoverInteressado(Usuario usuario)
        {
            if (!Interessados.Contains(usuario))
                AdicionarMensagem("Usuário não está interessado neste produto.");

            Interessados.Remove(usuario);   
        }

        public void MarcarVendido(Usuario usuario)
        {
            this.Comprador = usuario;
            this.Status = "f";
        }
    }
}
