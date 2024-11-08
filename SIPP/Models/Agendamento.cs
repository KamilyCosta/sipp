using System.ComponentModel.DataAnnotations.Schema;

namespace SIPP.Models
{

    public class Agendamento
    {
        public Guid AgendamentoId { get; set; }

        public DateOnly DataAge { get; set; }
        public TimeOnly HoraAge { get; set; }

        [NotMapped]
        public DateTime DataHoraAge => DataAge.ToDateTime(HoraAge);


        public Guid ClienteId { get; set; }
        public Pessoa Cliente { get; set; }


        public Guid CorretorId { get; set; }
        public Pessoa Corretor { get; set; }

    }
}


