using System;
using System.ComponentModel.DataAnnotations;

namespace Padaria.Application.ViewModels
{
    public class ProdutoPublicDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Range(0, 100000)]
        public double Preco { get; set; }
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ImagemUrl { get; set; }

    }
}
