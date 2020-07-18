using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DAL.Models
{
    public class Asiento
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaGenerado { get; set; }

        public long CuentaContable { get; set; }

        public int Monto { get; set; }

        public string TipoMovimiento { get; set; }
    }
}
