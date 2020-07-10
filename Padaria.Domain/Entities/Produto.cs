using System;
using System.Collections.Generic;

namespace Padaria.Domain.Entities
{
    public class Produto
    {
        public Guid Id {get; set;}
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public string ImagemUrl { get; set; }
        public List<Venda> Vendas { get; set; }

        

    }
}