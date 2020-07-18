using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using Payroll.DAL.Implementations;
using Payroll.DAL.Abstractions;
using Payroll.DAL;
using Payroll.UTL.Exceptions;
using Payroll.UTL.Models;
using FileHelpers;
using System.Web.Script.Serialization;
using Payroll.DAL.Models;
using Newtonsoft.Json;

namespace Payroll.BOL
{
    public class FileManagementService
    {
        private readonly EmployeeService _employeeService = new EmployeeService();


        public string GetOutPutJsonFile()
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\output.json";
            using (StreamReader r = new StreamReader(path))
            {
                return r.ReadToEnd();
                //return JsonConvert.DeserializeObject<List<Asiento>>(json);
            }
        }

        public void GenerateOutPutJsonFile()
        {
            var asientoList = _employeeService.GetAll().ToList();
            string jsondata = new JavaScriptSerializer().Serialize(asientoList);
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\";
            File.WriteAllText(path + "output.json", jsondata);
        }

        public void GenerateAsiento(Asiento asiento)
        {
            _employeeService.Add(asiento);
            //string jsondata = new JavaScriptSerializer().Serialize(asientoList);
            //string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\";
            //File.WriteAllText(path + "output.json", jsondata);
        }

        public string GenerateOutPutFile(DateTime payrollDate)
        {
            var employees = _employeeService.GetAll("PayrollsSheet");

            if (employees == null || employees.Count() == 0)
                throw new HttpStatusException($"No hay empleados disponibles. Favor revisar la fuente de datos.",
                    HttpStatusCode.Forbidden);

            var totalAmount = employees?.Sum(x => x.PayrollsSheet?
                .FirstOrDefault(p => p.PayrollDate.Date == payrollDate.Date)?.NetSalary);

            if (totalAmount == null || totalAmount < 1)
                throw new HttpStatusException($"No hay nominas disponibles para la fecha indicada {payrollDate:dd/MM/yyyy}",
                    HttpStatusCode.Forbidden);

            var payrollHeader = new List<EmployeePayrollHeader>()
            {
                new EmployeePayrollHeader()
                {
                    CompanyRnc = "401005107",
                    PayrollPaymentDay = $"{payrollDate.Date:dd/MM/yyyy}",
                    RowsQuantity = employees.Count().ToString(),
                    SourceAccountNumber = "11112681455",
                    TotalAmount = totalAmount.ToString(),
                    TransmissionDate = $"{DateTime.Now:dd/MM/yyyy}"
                }
            };

            var fhEngineHeader = new FileHelperEngine<EmployeePayrollHeader>();

            var fhEngineDetails = new FileHelperEngine<EmployeePayrollDetail>()
            {
                Encoding = new UTF8Encoding(false),
                HeaderText = fhEngineHeader.WriteString(payrollHeader)
            };

            List<EmployeePayrollDetail> payrollList = new List<EmployeePayrollDetail>();

            employees.ToList().ForEach(item =>
            {
                var payroll = item.PayrollsSheet.FirstOrDefault(x => x.PayrollDate.Date == payrollDate.Date);
                if (payroll != null)
                {
                    var employeePayroll = new EmployeePayrollDetail()
                    {
                        NetIncome = payroll.NetSalary.ToString(),
                        AccountNumber = item.AccountNumber,
                        DocumentNumber = item.DocumentNumber
                    };
                    payrollList.Add(employeePayroll);
                }
            });

            string fileName = $"Nomina{payrollDate.Date.ToShortDateString()}.txt".Replace("/",string.Empty);

            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                fhEngineDetails.WriteStream(streamWriter, payrollList);
                streamWriter.AutoFlush = true;
                stream.Position = 0;

                using (var fileStream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\{fileName}", FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public string GetOutPutFile(string fileName)
        {
            var fullFileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\{fileName}.txt";

            if (!File.Exists(fullFileName))
                throw new HttpStatusException($"El archivo con el nombre indicado: {fileName} no existe.",
                    HttpStatusCode.Forbidden);

            var fileLines = File.ReadAllLines(fullFileName);

            return fileLines.ArrayToString();
        }
    }
}
