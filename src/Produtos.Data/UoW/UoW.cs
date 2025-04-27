using Produtos.Domain.Interfaces.UoW;

namespace Produtos.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
