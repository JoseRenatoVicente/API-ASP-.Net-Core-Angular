using Padaria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Padaria.Repository
{
    public interface IPadariaRepository
    {
        //Geral
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;

        Task<bool> SaveChangesAsync();

        //Pedido
        Task<Pedido[]> GetAllPedidosAsync();
        Task<Pedido[]> GetAllPedidosAsyncById(Guid PedidoId);

        //Produto
        Task<Produto[]> GetAllProdutosAsync();
        Task<Produto> GetAllProdutosAsyncById(Guid ProdutoId);
    }
}
