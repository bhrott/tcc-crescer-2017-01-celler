﻿using System.Collections.Generic;

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
    }
}
