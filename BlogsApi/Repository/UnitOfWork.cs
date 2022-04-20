using BlogsApi.Database;
using BlogsApi.IRepository;
using BlogsApi.Model;
using System;
using System.Threading.Tasks;

namespace BlogsApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IBaseRepository<Category> _categoryRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

        }
        public IBaseRepository<Category> Categories => _categoryRepository ??= new BaseRepository<Category>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
