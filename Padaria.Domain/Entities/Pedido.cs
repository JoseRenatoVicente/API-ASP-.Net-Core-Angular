using System;
using System.Collections.Generic;

namespace Padaria.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public double ValorTotal { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string Qrcode { get; set; }
        public Guid ClienteId { get; set; }//fk cliente 
        public Cliente Cliente { get;}//deve readonly
        public List<Venda> Vendas { get; set; }

        
    }
}