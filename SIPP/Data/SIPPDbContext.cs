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

           
        }


    
    }
}

