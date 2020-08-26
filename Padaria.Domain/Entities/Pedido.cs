using Padaria.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Padaria.Domain.Entities
{
    public class Pedido : EntityBase
    {
        public double ValorTotal { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string Qrcode { get; set; }
        public Cliente Cliente { get;}//deve readonly
        public List<Venda> Vendas { get; set; }

        
    }
}