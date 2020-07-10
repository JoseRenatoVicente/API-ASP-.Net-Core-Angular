using Microsoft.EntityFrameworkCore;
using Padaria.Domain.Entities;
using Padaria.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Padaria.Repository
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
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
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

        //Pedido
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
