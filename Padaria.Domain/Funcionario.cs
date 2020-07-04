using System;

namespace Padaria.Domain
{
    public class Funcionario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Acesso { get; set; }

       
    }
}