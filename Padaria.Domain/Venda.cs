using System;

namespace Padaria.Domain
{
    public class Venda
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }//FK
        public Produto Produto { get; }//deve readonly
        public Guid PedidoId { get; set; }//FK
        public Pedido Pedido { get; }//deve readonly
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
        
     
    }
}