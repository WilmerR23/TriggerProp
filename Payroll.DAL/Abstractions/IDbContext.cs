using System.Data.Entity;
using System.Threading.Tasks;

namespace Payroll.DAL.Abstractions
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
