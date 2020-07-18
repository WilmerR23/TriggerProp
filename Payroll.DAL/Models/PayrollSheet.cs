using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.DAL.Models
{
    public class PayrollSheet
    {
        public int Id { get; set; }
        public DateTime PayrollDate { get; set; }
        public double? ISRDiscount { get; set; }
        public double? AFPDiscount { get; set; }
        public double? OthersDiscounts { get; set; }
        public double? TotalDiscounts { get; set; }
        public double? NetSalary { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
