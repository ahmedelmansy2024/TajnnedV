using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {

        void Update(T entity);
        IQueryable<T> GetAll(bool noTracking = true);
        Task<T?> GetByIdAsync(Guid id);
        void Insert(T entity);
        void Insert(List<T> entities);
        void Delete(T entity);
        void Remove(IEnumerable<T> entitiesToRemove);

        Task<IEnumerable<T>> ExecuteSqlAsync(string sql, params SqlParameter[] parameters);

    }
}
