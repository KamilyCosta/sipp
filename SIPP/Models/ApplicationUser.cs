
using Microsoft.AspNetCore.Identity;

namespace SIPP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; } // "Corretor" ou "Cliente"
        public ICollection<Pessoa> Pessoas { get; set; } // Relação com Pessoa
    }

    
}
