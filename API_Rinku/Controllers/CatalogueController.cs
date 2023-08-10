using CommonBase.Helpers;
using Services.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace API_Rinku.Controllers
{
    public class CatalogueController : ApiController
    {
        [HttpGet]
        public ApiResponse GetAll_Months()
        {
            var response = new ApiResponse();

            CatalogueManager manager = new CatalogueManager();
            try
            {
                var data = manager.GetAll_Months();
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
        public ApiResponse GetAll_Rol()
        {
            var response = new ApiResponse();

            CatalogueManager manager = new CatalogueManager();
            try
            {
                var data = manager.GetAll_Rol();
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