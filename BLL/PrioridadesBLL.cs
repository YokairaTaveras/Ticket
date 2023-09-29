using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ticket.DAL;
using Ticket.Models;

namespace Ticket.BLL
{
    public class PrioridadesBLL
    {
        private Context _context;
        public PrioridadesBLL(Context context)
        {
            _context = context;
        }
        public bool Existe(int PrioridadId)
        {
            return _context.Prioridades.Any(o => o.PrioridadId == PrioridadId);
        }
        private bool Insertar(Prioridades Prioridades)
        {
            _context.Prioridades.Add(Prioridades);
            return _context.SaveChanges() > 0;
        }
        private bool Modificar(Prioridades Prioridades)
        {
            var existe = _context.Prioridades.Find(Prioridades.PrioridadId);
            if (existe != null)
            {
                _context.Entry(existe).CurrentValues.SetValues(Prioridades);
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public bool Guardar(Prioridades Prioridades)
        {
            if (!Existe(Prioridades.PrioridadId))
                return this.Insertar(Prioridades);
            else
                return this.Modificar(Prioridades);
        }
        public bool Eliminar(int PrioridadesId)
        {
            var eliminado = _context.Prioridades.Where(o => o.PrioridadId == PrioridadesId).SingleOrDefault();

            if (eliminado != null)
            {
                _context.Entry(eliminado).State = EntityState.Deleted;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public Prioridades? Buscar(int PrioridadId)
        {
            return _context.Prioridades.Where(o => o.PrioridadId == PrioridadId).AsNoTracking().SingleOrDefault();
        }
        public List<Prioridades> GetList(Expression<Func<Prioridades, bool>> criterio)
        {
            return _context.Prioridades.AsNoTracking().Where(criterio).ToList();
        }
    }
}
