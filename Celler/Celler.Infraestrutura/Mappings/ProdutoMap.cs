using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Mappings
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produto");

            HasOptional(x => x.Comprador)
                .WithMany()
                .Map(x => x.MapKey("IdComprador"));

            HasMany(x => x.Interessados)
                 .WithMany()
                 .Map(x =>
                 {
                     x.MapLeftKey("IdProduto");
                     x.MapRightKey("IdUsuario");
                     x.ToTable("InteressadoProduto");
                 });
        }
    }
}
