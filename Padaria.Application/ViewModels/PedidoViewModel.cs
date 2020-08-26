using System;
using System.Collections.Generic;

namespace Padaria.Application.ViewModels
{
    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public double ValorTotal { get; set; }
        public string DataInicio { get; set; }
        public string DataFinal { get; set; }
        public string Qrcode { get; set; }
        public Guid ClienteId { get; set; }//fk cliente 
        public ClienteViewModel Cliente { get; }//deve readonly
        public List<VendaViewModel> Vendas { get; set; }
    }
}
