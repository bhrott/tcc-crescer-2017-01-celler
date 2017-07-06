using Celler.Dominio.Entidades;
using Celler.Infraestrutura.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=Connection")
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<Produto> Produto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new NotificacaoMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
        }
    }
}
