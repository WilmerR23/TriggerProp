using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.UTL.Models
{
    public static class Extension
    {
        public static string ArrayToString(this string[] lines)
        {
            string cadena = null;
            for (int x = 0; x < lines.Length; x++)
            {
                cadena +=  $"{lines[x]}\n";
            }
            return cadena;
        }
    }
}
