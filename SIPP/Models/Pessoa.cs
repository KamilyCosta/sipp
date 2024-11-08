using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace SIPP.Models
{
    public class Pessoa
    {

        [Key]
        public Guid PessoaId { get; set; }
        public string? Nome { get; set; }


        [RegularExpression(@"\d{11}", ErrorMessage = "O CPF deve ter 11 dígitos.")]
        public string? CPF { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string? CEP { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public string? Complemento { get; set; }
        public int? Numero { get; set; }

        public string? Telefone { get; set; }

        public string? CRECI { get; set; }

        public string? UrlImagem { get; set; }
        public DateOnly? DataCadastro { get; set; }

        public string? UserId { get; set; }

        // Relacionamento com TipoPessoa
        public Guid? TipoPessoaId { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public string? Email { get; set; }

        [NotMapped]
        public string? Senha { get; set; }

        public ICollection<Agendamento>? AgendamentosCliente { get; set; }
        public ICollection<Agendamento>? AgendamentosCorretor { get; set; }

    }
}
