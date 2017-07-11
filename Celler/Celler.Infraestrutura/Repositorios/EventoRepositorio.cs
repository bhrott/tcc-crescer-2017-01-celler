using Celler.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace Celler.Infraestrutura.Repositorios
{
    public class EventoRepositorio
    {
        readonly Contexto _contexto;

        public EventoRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }
        public void Dispose()
        {
            _contexto.Dispose();
        }

        public Evento ObterPorId(int idEvento)
        {
            return _contexto.Evento
                .Include(e => e.Confirmados)
                .Include(e => e.Criador)
                .FirstOrDefault(e => e.Id == idEvento);
        }

        public void Alterar(Evento evento)
        {
            _contexto.Entry(evento).State = EntityState.Modified;
        }
    }
}
