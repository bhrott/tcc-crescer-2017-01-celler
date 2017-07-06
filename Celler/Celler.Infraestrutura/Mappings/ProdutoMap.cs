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

            HasRequired(x => x.Criador)
                .WithMany()
                .Map(x => x.MapKey("IdCriador"));

            HasOptional(x => x.Comprador)
                .WithMany()
                .Map(x => x.MapKey("IdComprador"));

            //Verificar Porque não está ficando opcional na Migration
            Property(x => x.Foto1).IsOptional();
            Property(x => x.Foto2).IsOptional();
            Property(x => x.Foto3).IsOptional();
        }
    }
}
