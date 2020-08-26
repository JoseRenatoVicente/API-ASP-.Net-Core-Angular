using Padaria.Domain.Entities.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Padaria.Domain.Entities
{
    public class Produto : EntityBase
    {
        public Produto(string nome, double preco, string descricao, string status, string imagemUrl)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            Status = status;
            ImagemUrl = imagemUrl;
        }

        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public string ImagemUrl { get; set; }
        public List<Venda> Vendas { get; set; }
        

    }
}