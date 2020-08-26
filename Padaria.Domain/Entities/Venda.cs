using Padaria.Domain.Entities.Base;
using System;

namespace Padaria.Domain.Entities
{
    public class Venda : EntityBase
    {
        public Produto Produto { get; }//deve readonly
        public Pedido Pedido { get; }//deve readonly
        public int Quantidade { get; set; }
        public double ValorProduto { get; set; }
        
     
    }
}