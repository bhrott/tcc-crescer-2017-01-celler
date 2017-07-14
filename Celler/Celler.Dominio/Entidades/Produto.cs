using System;
using System.Collections.Generic;

namespace Celler.Dominio.Entidades
{
    public class Produto : Anuncio
    {
        public double Valor { get; private set; }
        public Usuario Comprador { get; private set; }
        public List<Usuario> Interessados { get; set; }

        protected Produto() { }

        public static readonly string Erro_Usuario_Ja_Interessado = "Usuário já está interessado neste produto.";
        public static readonly string Erro_Proprio_Produto = "O criador não pode se interessar pelo seu próprio produto";
        public static readonly string Erro_Produto_Vendido = "Você não pode se interessar por um produto vendido";
        public static readonly string Erro_Usuario_Nao_Interessado = "Usuário não está interessado neste produto.";
        public static readonly string Erro_Preco_Inferior_5Reais = "Produto com valor inferior a R$5.00.";

        public Produto(string titulo, string descricao, string foto1, string foto2, string foto3, Usuario usuarioLogado, double valor)
            : base(titulo, descricao, usuarioLogado, Entidades.TipoAnuncio.PRODUTO, foto1, foto2, foto3)
        {
            this.Valor = valor;

            if (valor < 5)
                AdicionarMensagem(Erro_Preco_Inferior_5Reais);
        }

        public override int GetNumeroPessoasComInteresse()
        {
            return Interessados.Count;
        }

        public void AdicionarInteressado(Usuario usuario)
        {
            if (Interessados.Contains(usuario))
                AdicionarMensagem(Erro_Usuario_Ja_Interessado);

            else if (Criador.Equals(usuario))
                AdicionarMensagem(Erro_Proprio_Produto);

            else if (Status == "f")
                AdicionarMensagem(Erro_Produto_Vendido);

            else
                Interessados.Add(usuario);

        }

        public void RemoverInteressado(Usuario usuario)
        {
            if (!Interessados.Contains(usuario))
                AdicionarMensagem(Erro_Usuario_Nao_Interessado);

            Interessados.Remove(usuario);   
        }

        public void MarcarVendido(Usuario usuario)
        {
            this.Comprador = usuario;
            this.Status = "f";
        }

        public void Alterar(Produto produto, string titulo, string descricao, string foto1, string foto2, string foto3, double valor)
        {
            produto.Titulo = titulo;
            produto.Descricao = descricao;
            produto.Foto1 = foto1;
            produto.Foto2 = foto2;
            produto.Foto3 = foto3;
            produto.Valor = valor;
        }
    }
}
