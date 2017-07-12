using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class VaquinhaModelDetalhes : AnuncioModelDetalhes
    {
        public double ArrecadamentoPrevisto { get; set; }
        public double TotalArrecadado { get; set; }
        public DateTime DataTermino { get; set; }
        public List<DoadorModel> Doadores { get; set; }
        public int NumeroDoadores { get; set; }

        public VaquinhaModelDetalhes(int id,
                                   string titulo,
                                   string descricao,
                                   DateTime dataAnuncio,
                                   string tipoAnuncio,
                                   string foto1,
                                   string foto2,
                                   string foto3,
                                   UsuarioModel criador,
                                   string status)
                                   :base (id,
                                          titulo,
                                          descricao,
                                          dataAnuncio,
                                          tipoAnuncio,
                                          foto1,
                                          foto2,
                                          foto3,
                                          criador,
                                          status)
        { }

        public VaquinhaModelDetalhes(Anuncio anuncio)
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

        public void PopularConfirmados(Vaquinha vaquinha)
        {
            Doadores = new List<DoadorModel>();

            foreach (var doador in vaquinha.Doadores)
            {
                this.Doadores.Add(new DoadorModel( doador.Id,
                                                   doador.ValorDoado,
                                                   doador.Status,
                                                   new UsuarioModel(doador.Usuario.Id,
                                                                    doador.Usuario.Nome,
                                                                    doador.Usuario.Email)));
            }
            ContarConfirmados(vaquinha);
        }

        public void ContarConfirmados(Vaquinha vaquinha)
        {
            if (Doadores == null) { Doadores = new List<DoadorModel>(); }
            this.NumeroDoadores = vaquinha.Doadores.Count;
        }
    }
}
