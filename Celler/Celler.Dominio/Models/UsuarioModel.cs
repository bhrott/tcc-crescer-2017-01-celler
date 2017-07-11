using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Dominio.Models
{
    public class UsuarioModel
    {
        public UsuarioModel(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
