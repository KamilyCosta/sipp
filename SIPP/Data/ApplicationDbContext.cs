using Microsoft.EntityFrameworkCore;
using SIPP.Models;

namespace SIPP.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Corretor> Corretores { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbCliente");
            modelBuilder.Entity<Imovel>().ToTable("tbImovel");
            modelBuilder.Entity<Agendamento>().ToTable("tbAgendamento");
            modelBuilder.Entity<Corretor>().ToTable("tbCorretor");
            


        }

    }
}
