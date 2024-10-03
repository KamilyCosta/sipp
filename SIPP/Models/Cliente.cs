using Microsoft.EntityFrameworkCore;

namespace SIPP.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public int CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public int CEP { get; set; }
        public string Bairro { get; set; }
        public string rua { get; set; }
        public string complemento { get; set; }
        public int numero { get; set; }

        public string campoTeste { get; set; } 
    }
}

