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
    
        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Notificacao> Notificacao { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Doador> Doador { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Vaquinha> Vaquinha { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            modelBuilder.Configurations.Add(new AnuncioMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new NotificacaoMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new ComentarioMap());
            modelBuilder.Configurations.Add(new DoadorMap());
            modelBuilder.Configurations.Add(new EventoMap());
            modelBuilder.Configurations.Add(new VaquinhaMap());
        }
    }
}
