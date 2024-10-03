using System.ComponentModel.DataAnnotations.Schema;

namespace SIPP.Models
{
   
    public class Imoveis
    {
        public Guid ImovelId { get; set; }
        public string Endereco { get; set; }
        public string cidade { get; set; }
        public string QntDormitorios { get; set; } 
        public string QntGarragem { get; set; }
        public string Tamanho { get; set; }
        public string TamanhoAreaContuida { get; set; }
        public string MetodoPagamento { get; set; }
        public decimal Valor { get; set; }
    }
}
