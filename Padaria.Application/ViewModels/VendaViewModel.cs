using System;

namespace Padaria.Application.ViewModels
{
    public class VendaViewModel
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }//FK
        public ProdutoViewModel Produto { get; }//deve readonly
        public Guid PedidoId { get; set; }//FK
        public PedidoViewModel Pedido { get; }//deve readonly
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
    }
}
