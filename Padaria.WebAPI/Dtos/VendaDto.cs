using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Dtos
{
    public class VendaDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }//FK
        public ProdutoDto Produto { get; }//deve readonly
        public Guid PedidoId { get; set; }//FK
        public PedidoDto Pedido { get; }//deve readonly
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
    }
}
