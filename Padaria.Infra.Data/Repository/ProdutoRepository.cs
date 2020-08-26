using Padaria.Domain.Entities;
using Padaria.Infra.Data.Context;
using Padaria.Infra.Data.Repository.Base;

namespace Padaria.Infra.Data.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
