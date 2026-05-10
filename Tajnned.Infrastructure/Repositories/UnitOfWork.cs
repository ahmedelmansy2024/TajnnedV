

using Tajnned.Domain.Interfaces.Repositories;
using Tajnned.Infrastructure.Data;

namespace Tajnned.Infrastructure.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
       
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

         

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
