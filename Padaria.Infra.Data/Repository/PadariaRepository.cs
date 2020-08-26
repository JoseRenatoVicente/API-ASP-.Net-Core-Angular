using Microsoft.EntityFrameworkCore;
using Padaria.Domain.Entities;
using Padaria.Infra.Data.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Padaria.Infra.Data.Repository
{
    public class PadariaRepository : IPadariaRepository
    {
       

        private readonly DataContext _context;
        public PadariaRepository(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //Gerais
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }
        public async Task AddRangeAsync<T>(T[] entity) where T : class
        {
            await _context.AddRangeAsync(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Pedidos
        public async Task<Pedido[]> GetAllPedidosAsync()
        {
            IQueryable<Pedido> query = _context.Pedidos
                .Include(c => c.Vendas);

            query = query.AsNoTracking()
                .OrderByDescending(c => c.DataInicio);

            return await query.ToArrayAsync();
        }

        public async Task<Pedido[]> GetAllPedidosAsyncById(Guid PedidoId)
        {
            IQueryable<Pedido> query = _context.Pedidos
                 .Include(c => c.Vendas);

            query = query.AsNoTracking()
                .OrderByDescending(c => c.DataInicio)
                .Where(c => c.Id == PedidoId);


            return await query.ToArrayAsync();
        }

        //Produtos

        public async Task<Produto[]> GetAllProdutosAsync()
        {
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking()
                .Where(c => c.Status == "1");
                

            return await query.ToArrayAsync();
        }


        public async Task<Produto[]> GetAllProdutosAsyncAdmin()
        {
            IQueryable<Produto> query = _context.Produtos
                .Include(c => c.Vendas);

            query = query.AsNoTracking()
                .OrderBy(c => c.Nome);//Traz em ordem alfabetica pelo nome


            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetAllProdutosAsyncById(Guid ProdutoId)
        {
            IQueryable<Produto> query = _context.Produtos
                 .Include(c => c.Vendas);

            query = query.AsNoTracking()
                .Where(c => c.Id == ProdutoId);


            return await query.FirstOrDefaultAsync();
        }

    }
}
