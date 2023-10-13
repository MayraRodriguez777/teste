using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioCliente> UsuariosClientes { get; set; }
        public DbSet<UsuarioDentista> UsuariosDentistas { get; set; }
        public DbSet<AgendaEvento> AgendaEventos { get; set; }

        // Outros DbSets para outras entidades, se houver

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaEvento>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<AgendaEvento>()
                .Property(e => e.Titulo)
                .IsRequired();

            modelBuilder.Entity<AgendaEvento>()
                .Property(e => e.DataHora)
                .IsRequired();

            // Configurar relacionamentos
            modelBuilder.Entity<AgendaEvento>()
                .HasOne(e => e.UsuarioCliente)
                .WithMany(u => u.AgendaEventos)
                .HasForeignKey(e => e.UsuarioClienteId)
                .IsRequired();

            modelBuilder.Entity<AgendaEvento>()
                .HasOne(e => e.UsuarioDentista)
                .WithMany(d => d.AgendaEventos)
                .HasForeignKey(e => e.UsuarioDentistaId)
                .IsRequired();

            // Configurar outras entidades e relacionamentos, se houver

            base.OnModelCreating(modelBuilder);
        }
    }
}
