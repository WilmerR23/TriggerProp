using System.Collections.Generic;

namespace Payroll.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? CrudeSalary { get; set; }
        public string AccountNumber { get; set; }

        public virtual IList<PayrollSheet> PayrollsSheet { get; set; }
    }
}
