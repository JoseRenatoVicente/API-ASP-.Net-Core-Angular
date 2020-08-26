using System.Collections.Generic;

namespace Padaria.Domain.Entities.Base.Helpers
{
    public class Paginacao<T> where T : class
    {
        public int NumeroPagina { get; set; }
        public int RegistroPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }

        public List<T> Data { get; set; }
    }
}
