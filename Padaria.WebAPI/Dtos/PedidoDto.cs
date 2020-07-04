using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Dtos
{
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public double ValorTotal { get; set; }
        public string DataInicio { get; set; }
        public string DataFinal { get; set; }
        public string Qrcode { get; set; }
        public Guid ClienteId { get; set; }//fk cliente 
        public ClienteDto Cliente { get; }//deve readonly
        public List<VendaDto> Vendas { get; set; }
    }
}
