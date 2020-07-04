using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Dtos
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Range(0, 100000)]
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ImagemUrl { get; set; }
        //public List<VendaDto> Vendas { get; set; }
    }
}
