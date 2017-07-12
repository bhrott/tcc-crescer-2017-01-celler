using Celler.Dominio.Entidades;
using Celler.Dominio.Models;
using System.Data.Entity;
using System.Linq;

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

        public AnuncioModelDetalhes ObterDetalhes(int idEvento)
        {
            Evento evento = ObterPorId(idEvento);
            EventoModelDetalhes eventoModel = new EventoModelDetalhes(evento);
            eventoModel.SetarInformacoesEspecificas(evento);
            eventoModel.PopularComentarios(evento);
            eventoModel.PopularConfirmados(evento);

            return eventoModel;
        }
    }
}
