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
    public class ProcessController : ApiController
    {
        [HttpPost]
        public ApiResponse SaveMovements(MovementsRequest movements)
        {
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                var data = manager.SaveMovements(movements.EmployeeId, movements.DeliveryQuantity, movements.MonthId);
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

        [HttpGet]
        public ApiResponse GetMovementById(Int64 Id)
        {
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                var data = manager.GetMovementById(Id);
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

        [HttpPut]
        public ApiResponse UpdateMovement(MovementsRequest movements)
        {
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                manager.UpdateMovement(movements);
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
        public ApiResponse InactiveMovementById(Int64 Id)
        {
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                manager.InactiveMovementById(Id);
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
        public ApiResponse CalculateSalaryAndCompensation(Int64 EmployeeId, int MonthId)
        {
            CalculationRequest movements = new CalculationRequest();

            movements.EmployeeId = EmployeeId;
            movements.MonthId = MonthId;    
            var response = new ApiResponse();

            ProcessManager manager = new ProcessManager();
            try
            {
                var data = manager.CalculateSalaryAndCompensation(movements);
                response.Data = data;
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
    }
}