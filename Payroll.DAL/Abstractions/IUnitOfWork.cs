using System;
using System.Threading.Tasks;

namespace Payroll.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
        Task<int> SaveAsync();
    }
}
