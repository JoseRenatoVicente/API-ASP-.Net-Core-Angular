using System;
using System.Collections.Generic;

namespace Padaria.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public List<Pedido> Pedidos { get; set; }

        
    }
}