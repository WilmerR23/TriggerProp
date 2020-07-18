using Payroll.DAL;
using Payroll.DAL.Models;
using Payroll.DAL.Implementations;
using System.Collections.Generic;

namespace Payroll.BOL
{
    public class EmployeeService
    {
        private UnitOfWork<AppDbContext> _unitOfWork = new UnitOfWork<AppDbContext>();

        public IEnumerable<Asiento> GetAll() => _unitOfWork.GetRepository<Asiento>().GetAll();

        public IEnumerable<Employee> GetAll(string include) => 
            _unitOfWork.GetRepository<Employee>().GetAllWithInclude(include);

        public Employee FindByID(long id) => _unitOfWork.GetRepository<Employee>().Get(t => t.Id == id);

        public bool Exists(long id) => _unitOfWork.GetRepository<Employee>().Get(t => t.Id == id) != null;

        public void Add(Asiento entity)
        {
            _unitOfWork.GetRepository<Asiento>().Add(entity);
            _unitOfWork.Save();
        }

        public void Update(Employee entity)
        {
            _unitOfWork.GetRepository<Employee>().Update(entity);
            _unitOfWork.Save();
        }

        public void Delete(Employee employee) => _unitOfWork.GetRepository<Employee>().Delete(employee);

    }
}
