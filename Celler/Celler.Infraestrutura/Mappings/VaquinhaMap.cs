using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Mappings
{
    class VaquinhaMap : EntityTypeConfiguration<Vaquinha>
    {
        public VaquinhaMap()
        {
            ToTable("Vaquinha");

            HasMany(x => x.Doadores)
                 .WithMany()
                 .Map(x =>
                 {
                     x.MapLeftKey("IdVaquinha");
                     x.MapRightKey("IdDoador");
                     x.ToTable("DoadorVaquinha");
                 });

        }
    }
}
