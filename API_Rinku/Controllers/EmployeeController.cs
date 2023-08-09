using CommonBase.Helpers;
using Services.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Rinku.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpPost]
        public ApiResponse SaveEmployee(string Name, double HourlyWage, Int64 RolId)
        {
            var response = new ApiResponse();

            EmployeeManager manager = new EmployeeManager();
            try 
            {
                var data = manager.SaveEmployee(Name, HourlyWage, RolId);
                response.Message = data.Name;
                response.IsError = false;
            }
            catch (Exception ex) {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        public string SaveMovements()
        {
            return "";
        }


    }
}