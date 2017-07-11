using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celler.Api.Models
{
    public class InteressarProdutoModel : EntidadeBasica
    {
        public int idUsuario { get; set; }

        public int IdProduto{ get; set; }

        public override bool Validar()
        {
            if (idUsuario==0 || IdProduto==0)
            {
                Mensagens.Add("Os id's não foram informados corretamente.");
            }

            return Mensagens.Count == 0;
        }
    }
}