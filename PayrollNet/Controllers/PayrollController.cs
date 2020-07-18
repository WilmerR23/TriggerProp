using Payroll.BOL;
using Payroll.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PayrollNet.Controllers
{
    [RoutePrefix("api/Payroll")]
    public class PayrollController : ApiController
    {
        private FileManagementService _fMS = new FileManagementService();


        [HttpGet]
        [Route("GenPay")]
        public IHttpActionResult GeneratePayrollFile([FromBody] Asiento asiento)
        {
            try
            {
                asiento.FechaGenerado = DateTime.Now;
                _fMS.GenerateAsiento(asiento);
                var mensaje = $"El asiento fue generado.";
                return Ok(mensaje);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("ReadPay")]
        public HttpResponseMessage ReadPayrollFile()
        {
            var data = _fMS.GetOutPutJsonFile();
            return new HttpResponseMessage()
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };
        }
    }
}

