
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tajnned.Domain.Interfaces.Repositories;
using Tajnned.Infrastructure.Data;

namespace Tajnned.Infrastructure.Repositories

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entitySet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _entitySet = _context.Set<T>();
        }

        public IQueryable<T> GetAll(bool noTracking = true)
        {
            var set = _entitySet;
            if (noTracking)
            {
                return set.AsNoTracking();
            }
            return set;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _entitySet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _entitySet.Add(entity);
        }

        public void Insert(List<T> entities)
        {
            _entitySet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _entitySet.Remove(entity);
        }

        public void Remove(IEnumerable<T> entitiesToRemove)
        {
            _entitySet.RemoveRange(entitiesToRemove);
        }

        public void Update(T entity)
        {
            _entitySet.Update(entity);

        }

        public async  Task<IEnumerable<T>> ExecuteSqlAsync(string sql, params SqlParameter[] parameters)
         
             => await _entitySet.FromSqlRaw(sql, parameters).ToListAsync();
       
    }
}
