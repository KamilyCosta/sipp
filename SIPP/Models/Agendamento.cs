namespace SIPP.Models
{
    public class Agendamento
    {
        public Guid AgendamentoId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid CorretorId { get; set; }
        public DateOnly DataAge { get; set; }
        public TimeOnly HoraAge { get; set; }

    }
}
