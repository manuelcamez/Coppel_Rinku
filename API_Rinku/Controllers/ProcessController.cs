using CommonBase.Helpers;
using Services.DomainObject;
using Services.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace API_Rinku.Controllers
{
    public class ProcessController : ApiController
    {
        [HttpPost]
        public ApiResponse SaveMovements(Int64 EmployeeId, int DeliveryQuantity, int MonthId)
        {
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                var data = manager.SaveMovements(EmployeeId, DeliveryQuantity, MonthId);
                response.Message = data.Id.ToString();
                response.IsError = false;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}