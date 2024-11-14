using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIPP.Models;

namespace SIPP.Data
{
    public class SIPPDbContext : IdentityDbContext<IdentityUser>

    {

        public SIPPDbContext(DbContextOptions<SIPPDbContext> options) : base(options) { }

       
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<TipoPessoa> TipoPessoas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Cliente)
            .WithMany(p => p.AgendamentosCliente)
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);  

           
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Corretor)
                .WithMany(p => p.AgendamentosCorretor)
                .HasForeignKey(a => a.CorretorId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Imovel)  
            .WithMany(i => i.Agendamentos)
             .HasForeignKey(a => a.ImovelId); 

        }
   
    }
}

