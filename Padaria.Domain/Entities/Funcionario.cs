using Padaria.Domain.Entities.Base;
using System;

namespace Padaria.Domain.Entities
{
    public class Funcionario : EntityBase
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Acesso { get; set; }

       
    }
}