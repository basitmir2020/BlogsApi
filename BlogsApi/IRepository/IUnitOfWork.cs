using BlogsApi.Model;
using System;
using System.Threading.Tasks;

namespace BlogsApi.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Category> Categories { get; }
        Task Save();
    }
}
