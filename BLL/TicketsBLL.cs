using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ticket.DAL;
using Ticket.Models;

namespace Ticket.BLL
{
    public class TicketsBLL
    {
        private Context _context;
        public TicketsBLL(Context context)
        {
            _context = context;
        }
        public bool Existe(int TicketId)
        {
            return _context.Tickets.Any(o => o.TicketId == TicketId);
        }
        private bool Insertar(Tickets Tickets)
        {
            _context.Tickets.Add(Tickets);
            return _context.SaveChanges() > 0;
        }
        private bool Modificar(Tickets Tickets)
        {
            var existe = _context.Tickets.Find(Tickets.TicketId);
            if (existe != null)
            {
                _context.Entry(existe).CurrentValues.SetValues(Tickets);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public bool Guardar(Tickets Tickets)
        {
            if (!Existe(Tickets.TicketId))
                return this.Insertar(Tickets);
            else
                return this.Modificar(Tickets);
        }
        public bool Eliminar(int TicketId)
        {
            var eliminado = _context.Tickets.Where(o => o.TicketId == TicketId).SingleOrDefault();

            if (eliminado != null)
            {
                _context.Entry(eliminado).State = EntityState.Deleted;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public Tickets? Buscar(int TicketId)
        {
            return _context.Tickets.Where(o => o.TicketId == TicketId).AsNoTracking().SingleOrDefault();
        }
        public List<Tickets> GetList(Expression<Func<Tickets, bool>> criterio)
        {
            return _context.Tickets.AsNoTracking().Where(criterio).ToList();
        }
        public bool VerificarExistencia(Tickets Ticket)
        {
            var TicketIgual = _context.Tickets.Any(o => o.ClienteId == Ticket.ClienteId);

            if (TicketIgual)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
