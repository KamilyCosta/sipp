namespace SIPP.Models
{
    public class Agendamento
    {
        public Guid AgendamentoId { get; set; }
        public Guid PessoaId { get; set; } // Cliente
        // public Guid PessoaId { get; set; } // Corretor
        public DateOnly DataAge { get; set; }
        public TimeOnly HoraAge { get; set; }

    }
}
