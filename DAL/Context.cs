using Microsoft.EntityFrameworkCore;
using Ticket.Models;

namespace Ticket.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> Opcions) : base(Opcions) { }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
    }
}
