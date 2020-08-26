using Padaria.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Padaria.Application.ViewModels
{
    public class ProdutoViewModel : EntityBase
    {
        public string Nome { get; set; }
        [Range(0, 100000)]
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public string ImagemUrl { get; set; }
        //public List<VendaDto> Vendas { get; set; }
    }
}
