using Padaria.Domain.Entities.Base;
using Padaria.Domain.Entities.Base.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Padaria.Infra.Data.Repository.Base
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        //Geral
        Task<IQueryable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> ListarPor(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        Task<Paginacao<T>> GetPagingAsyncWhere(Expression<Func<T, bool>> where, int PagNumero = 1, int PagRegistro = 4, params Expression<Func<T, object>>[] includeProperties);
        Task<Paginacao<T>> GetPagingAsync(int PagNumero = 1, int PagRegistro = 4);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task AddRangeAsync(T[] entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(T[] entity);

        Task<bool> SaveChangesAsync();
    }
}
