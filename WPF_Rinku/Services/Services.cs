using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Rinku.Services
{
    public class Services
    {
        public async Task<string> EjecutarProceso()
        {
            string result = string.Empty;
            ConsumirApiRest ejecutarApi = new ConsumirApiRest();
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Employee/";
                string parametros = 1.ToString();
                string method = "GetEmployeeById/"+ parametros;
                

                var resultado = await ejecutarApi.ExecuteApiGetData(url, controller, method);

                switch (resultado)
                {
                    case "OK":
                        result = "OK";
                        break;
                    default:
                        result = "ErrorEnDB";
                        break;
                }
            }
            catch (HttpRequestException)
            {
                result = "ErrorApi";
            }
            catch (Exception ex)
            {

                result = ex.Message.ToString();

            }
            return result;
        }

        public async Task<string> SaveEmployee()
        {
            string result = string.Empty;
            ConsumirApiRest ejecutarApi = new ConsumirApiRest();
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "Employee/";
                string parametros = 1.ToString();
                string method = "GetEmployeeById/" + parametros;


                var resultado = await ejecutarApi.ExecuteApiGetData(url, controller, method);

                switch (resultado)
                {
                    case "OK":
                        result = "OK";
                        break;
                    default:
                        result = "ErrorEnDB";
                        break;
                }
            }
            catch (HttpRequestException)
            {
                result = "ErrorApi";
            }
            catch (Exception ex)
            {

                result = ex.Message.ToString();

            }
            return result;
        }
        public string Info()
        {
            string result = string.Empty;
            ConsumirApiRest ejecutarApi = new ConsumirApiRest();
            try
            {
                string url = ConfigurationManager.AppSettings["UrlService"].ToString();
                string controller = "RepVdoPtmo18_24/";
                string method = "Info";
                var resultado = ejecutarApi.ExecuteApiPost(url, controller, method);
                dynamic json = JsonConvert.DeserializeObject(resultado.Result);

                if (json.Message == "OK")
                {
                    result = json.Data;
                }
                else if (json.Message == "ErrorEnRuta")
                {
                    result = "ErrorEnRuta";
                }
                else
                {
                    result = "ErrorHelp";
                }
            }
            catch (Exception)
            {

                result = "ErrorHelp";

            }
            return result;
        }
    }
}
