namespace SIPP.Models
{
    public class Imagem
    { 
        public Guid ImagemId { get; set; } 
        public Guid ImovelId { get; set; } 
        public string Url { get; set; } 
       
        public virtual Imovel Imovel { get; set; }
    }
}
