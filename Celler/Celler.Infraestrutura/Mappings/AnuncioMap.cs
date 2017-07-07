using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Mappings
{
    class AnuncioMap : EntityTypeConfiguration<Anuncio>
    {
        public AnuncioMap()
        {
            ToTable("Anuncio");
            HasMany(x => x.Comentarios)
                .WithRequired()
                .Map(x => x.MapKey("IdAnuncio"))
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Criador)
              .WithMany()
              .Map(x => x.MapKey("IdCriador"));

        }
    }
}
