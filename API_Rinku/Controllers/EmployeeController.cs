using CommonBase.Helpers;
using Contracts.Request;
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
    public class EmployeeController : ApiController
    {

        [HttpPost]
        public ApiResponse SaveEmployee(EmployeeRequest employee)
        {
            
            var response = new ApiResponse();

            EmployeeManager manager = new EmployeeManager();
            try 
            {
                var data = manager.SaveEmployee(employee.Name, employee.HourlyWage, employee.RolId);
                response.Message = "OK";
                response.Data = data;
                response.IsError = false;
            }
            catch (Exception ex) {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPut]
        public ApiResponse UpdateEmployeeById(EmployeeRequest employee)
        {
            var response = new ApiResponse();

            EmployeeManager manager = new EmployeeManager();
            try
            {
                manager.UpdateEmployeeById(employee.Id, employee.Name, employee.HourlyWage, employee.RolId);
                response.Message = "OK";
                response.IsError = false;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPut]
        public ApiResponse InactiveEmployeeById(Int64 Id)
        {
            var response = new ApiResponse();

            EmployeeManager manager = new EmployeeManager();
            try
            {
                manager.InactiveEmployeeById(Id);
                response.Message = "OK";
                response.IsError = false;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet]
        public ApiResponse GetEmployeeById(Int64 Id)
        {
            var response = new ApiResponse();

            EmployeeManager manager = new EmployeeManager();
            try
            {
                var data = manager.GetEmployeeById(Id);
                response.Message = "OK";
                response.Data = data;
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