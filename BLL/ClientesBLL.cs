using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ticket.DAL;
using Ticket.Models;

namespace Ticket.BLL
{
    public class ClientesBLL
    {
        private Context _context;
        public ClientesBLL(Context context)
        {
            _context = context;
        }
        public bool Existe(int ClienteId)
        {
            return _context.Clientes.Any(o => o.ClienteId == ClienteId);
        }
        private bool Insertar(Clientes Clientes)
        {
            _context.Clientes.Add(Clientes);
            return _context.SaveChanges() > 0;
        }
        private bool Modificar(Clientes Clientes)
        {
            var existe = _context.Clientes.Find(Clientes.ClienteId);
            if (existe != null)
            {
                _context.Entry(existe).CurrentValues.SetValues(Clientes);
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public bool Guardar(Clientes Clientes)
        {
            if (!Existe(Clientes.ClienteId))
                return this.Insertar(Clientes);
            else
                return this.Modificar(Clientes);
        }
        public bool Eliminar(int ClientesId)
        {
            var eliminado = _context.Clientes.Where(o => o.ClienteId == ClientesId).SingleOrDefault();

            if (eliminado != null)
            {
                _context.Entry(eliminado).State = EntityState.Deleted;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
        public Clientes? Buscar(int ClienteId)
        {
            return _context.Clientes.Where(o => o.ClienteId == ClienteId).AsNoTracking().SingleOrDefault();
        }
        public List<Clientes> GetList(Expression<Func<Clientes, bool>> criterio)
        {
            return _context.Clientes.AsNoTracking().Where(criterio).ToList();
        }
        public bool VerificarExistencia(Clientes Cliente)
        {
            var clienteIgual = _context.Clientes.Any(o => o.Nombre == Cliente.Nombre || o.RNC == Cliente.RNC);
            if (clienteIgual)
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
