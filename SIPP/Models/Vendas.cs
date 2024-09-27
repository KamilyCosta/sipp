using System.ComponentModel.DataAnnotations.Schema;

namespace SIPP.Models
{
    [Table("tbVendas")]
    public class Vendas
    {
        public Guid IdVendas { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }

        public string Imagem { get; set; }
    }
}
