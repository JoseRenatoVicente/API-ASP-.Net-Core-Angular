using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Dtos
{
    public class ClienteDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ser entre 3 e 100 caracters")]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        
        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage ="O {0} é invalido")]
        public string Login { get; set; }
        //public string Senha { get; set; }
        public List<PedidoDto> Pedidos { get; set; }
    }
}
