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
            HasRequired(x => x.Criador)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"));
        }
    }
}
