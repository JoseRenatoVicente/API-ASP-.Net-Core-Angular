using Microsoft.EntityFrameworkCore;
using Padaria.Domain.Entities.Base;
using Padaria.Domain.Entities.Base.Helpers;
using Padaria.Infra.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Padaria.Infra.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T: EntityBase
    {
        private readonly DataContext _context;
        public BaseRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        //Gerais

        /*public virtual Task<IQueryable<T>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return _context.Set<T>().AsNoTracking();
            });
        }*/



        public virtual async Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _context.Set<T>();

            if (includeProperties.Any())
            {
                return await Include(_context.Set<T>(), includeProperties);
            }

            return query;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var iquerable = await GetAllAsync();

            return await iquerable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(T[] entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return Task.Run(() => { 
                _context.Update(entity);
            });
        }

        public virtual Task DeleteAsync(T entity)
        {
            return Task.Run(() => {
                _context.Remove(entity);
            });
        }

        public virtual Task DeleteRangeAsync(T[] entityArray)
        {
            return Task.Run(() => {
                _context.RemoveRange(entityArray);
            });
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        private Task<IQueryable<T>> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {

            return Task.Run(() => {
                foreach (var property in includeProperties)
                {
                    query = query.Include(property);
                }

                return query;
            });
        }

        public async Task<Paginacao<T>> GetPagingAsync(int PagNumero = 1, int PagRegistro = 4)
        {


            var iquerable = await GetAllAsync();

            var quantidadeTotalRegistros = iquerable.Count();
            var list = iquerable.Skip((PagNumero - 1) * PagRegistro).Take(PagRegistro).ToList();

            return new Paginacao<T>
            {
                NumeroPagina = PagNumero,
                RegistroPorPagina = PagRegistro,
                TotalRegistros = quantidadeTotalRegistros,
                TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / PagRegistro),
                Data = list
            };
        }

        public async Task<IQueryable<T>> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return (await GetAllAsync(includeProperties)).Where(where);
        }

        public async Task<Paginacao<T>> GetPagingAsyncWhere(Expression<Func<T, bool>> where, int PagNumero = 1, int PagRegistro = 4, params Expression<Func<T, object>>[] includeProperties)
        {
            var iquerable = (await GetAllAsync(includeProperties)).Where(where);

            var quantidadeTotalRegistros = iquerable.Count();
            var list = iquerable.Skip((PagNumero - 1) * PagRegistro).Take(PagRegistro).ToList();

            return new Paginacao<T>
            {
                NumeroPagina = PagNumero,
                RegistroPorPagina = PagRegistro,
                TotalRegistros = quantidadeTotalRegistros,
                TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / PagRegistro),
                Data = list
            };
        }
    }
}
