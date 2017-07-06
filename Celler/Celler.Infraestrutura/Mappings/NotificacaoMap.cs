using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Mappings
{
    public class NotificacaoMap : EntityTypeConfiguration<Notificacao>
    {
        public NotificacaoMap()
        {
            ToTable("Notificacao");
            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"));
        }
    }
}
