using Microsoft.EntityFrameworkCore;
using SIPP.Models;

namespace SIPP.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Imoveis> Imoveis { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Corretores> Corretores { get; set; }
        public DbSet<RelacionandoImoATipo> RelacionandoImoATipo { get; set; }
        public DbSet<TipodeTransacaso> TipodeTransacaso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Imoveis>().ToTable("tbImoveis");
            modelBuilder.Entity<Agendamento>().ToTable("tbAgendamento");
            modelBuilder.Entity<Corretores>().ToTable("tbCorretores");
            modelBuilder.Entity<RelacionandoImoATipo>().ToTable("tbRelacionandoImoATipo");
            modelBuilder.Entity<TipodeTransacaso>().ToTable("tbTipodeTransacaso");
        }

    }
}
