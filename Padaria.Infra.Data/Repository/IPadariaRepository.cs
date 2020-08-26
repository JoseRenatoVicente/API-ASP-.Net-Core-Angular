using Padaria.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Padaria.Infra.Data.Repository
{
    public interface IPadariaRepository
    {
        //Geral
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(T[] entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;

        Task<bool> SaveChangesAsync();

        //Pedido
        Task<Pedido[]> GetAllPedidosAsync();
        Task<Pedido[]> GetAllPedidosAsyncById(Guid PedidoId);

        //Produto
        Task<Produto[]> GetAllProdutosAsync();
        Task<Produto[]> GetAllProdutosAsyncAdmin();
        Task<Produto> GetAllProdutosAsyncById(Guid ProdutoId);
    }
}
