using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celler.Infraestrutura.Repositorios
{
    public class VaquinhaRepositorio
    {
        readonly Contexto _contexto;

        public VaquinhaRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public void Alterar(Vaquinha vaquinha)
        {
            _contexto.Entry(vaquinha).State = EntityState.Modified;
        }

        public Vaquinha ObterPorId(int id)
        {
            return _contexto.Vaquinha
                .Include(v => v.Doadores)
                .Include(v => v.Criador)
                .FirstOrDefault(v => v.Id == id);
        }

        public Doador ObterDoadorPorId(int IdDoacao)
        {
            return _contexto.Doador.FirstOrDefault(d => d.Id == IdDoacao);
        }
    }
}
