using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        // Outros DbSets para outras entidades, se houver
    }
}