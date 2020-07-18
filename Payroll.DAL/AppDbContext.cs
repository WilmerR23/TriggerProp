using Payroll.DAL.Abstractions;
using Payroll.DAL.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Payroll.DAL
{
    public class AppDbContext : DbContext,IDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayrollSheet> PayrollsSheet { get; set; }

        public DbSet<Asiento> Asiento { get; set; }
        //public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public AppDbContext() : base ("Unapec")
        {
        }

    }
}
