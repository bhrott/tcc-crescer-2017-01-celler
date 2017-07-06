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
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
            
        }
    }
}
