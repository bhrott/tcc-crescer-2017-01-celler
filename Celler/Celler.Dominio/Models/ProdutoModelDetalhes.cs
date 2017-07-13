using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class ProdutoModelDetalhes : AnuncioModelDetalhes
    {
        public double Valor { get; set; }
        public UsuarioModel Comprador { get; set; }
        public List<UsuarioModel> Interessados { get; set; }
        public int NumeroInteressados { get; set; }
        public bool TemInteresse { get; set; }

        public ProdutoModelDetalhes(Anuncio anuncio)
                                   : base(anuncio.Id,
                                          anuncio.Titulo,
                                          anuncio.Descricao,
                                          anuncio.DataAnuncio,
                                          anuncio.TipoAnuncio,
                                          anuncio.Foto1,
                                          anuncio.Foto2,
                                          anuncio.Foto3,
                                          new UsuarioModel (anuncio.Criador.Id, anuncio.Criador.Nome, anuncio.Criador.Email),
                                          anuncio.Status)
        { }

        public void SetarInformacoesEspecificas(Produto produto)
        {
            this.Valor = produto.Valor;
        }

        public void PopularConfirmados(Produto produto)
        {
            Interessados = new List<UsuarioModel>();

            foreach (var confirmados in produto.Interessados)
            {
                this.Interessados.Add(new UsuarioModel(confirmados.Id,
                                                       confirmados.Nome,
                                                       confirmados.Email));
            }
            ContarConfirmados(produto);
        }

        public void ContarConfirmados(Produto produto)
        {
            if (Interessados == null) { Interessados = new List<UsuarioModel>(); }
            this.NumeroInteressados = produto.Interessados.Count;
        }
    }
}
