using Microsoft.EntityFrameworkCore;

namespace Eassydentalmvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }

        // Outros DbSets para outras entidades, se houver
    }
}
