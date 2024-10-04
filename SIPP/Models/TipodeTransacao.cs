namespace SIPP.Models
{
    public class TipodeTransacao
    {
        public Guid TipodeTransacaoId {  get; set; }
        public string Tipo { get; set; }

        public ICollection<RelacionandoImoATipo> Relacionamentos { get; set; } = new List<RelacionandoImoATipo>();
    }
}
