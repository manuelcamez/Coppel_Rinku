using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Rinku.Services
{
    public class ConsumirApiRest
    {
        public async Task<string> ExecuteApiPostAsync(string url, string controller, string method, object parametros)
        {
            string result = string.Empty;
            try
            {
                dynamic json = "";
                string urlData = url + controller + method;
                var data = JsonConvert.SerializeObject(parametros);
                HttpClient clienteHttp = new HttpClient();
                clienteHttp.Timeout = TimeSpan.FromMinutes(120);
                clienteHttp.Timeout = TimeSpan.FromDays(3);
                HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var httpResponse = await clienteHttp.PostAsync(urlData, content);
                if (httpResponse.StatusCode.ToString() == "OK")
                {
                    var datos = await httpResponse.Content.ReadAsStringAsync();
                    json = JsonConvert.DeserializeObject(datos);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        if (json.Message == "OK")
                        {
                            result = "OK";
                        }
                        else if (json.Message == "ErrorEnRuta")
                        {
                            result = "ErrorEnRuta";
                        }
                        else
                        {
                            result = "ErrorEnDB";
                        }

                    }
                    else
                    {
                        result = "ErrorEnDB";
                    }
                }
                else
                {
                    result = "ErrorEnDB";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
        /// <summary> 
        ///  Metodo ExecuteApiPostAsync se usa para mandar una petición get al ApiRest 
        /// </summary> 
        public async Task<string> ExecuteApiPostAsync(string url, string controller, string method)
        {
            var result = "";
            try
            {
                string urlData = url + controller + method;

                dynamic json = "";
                HttpClient clienteHttp = new HttpClient();
                clienteHttp.Timeout = TimeSpan.FromDays(3);
                var httpResponse = await clienteHttp.GetAsync(urlData);

                if (httpResponse.StatusCode.ToString() == "OK")
                {
                    var datos = await httpResponse.Content.ReadAsStringAsync();
                    json = JsonConvert.DeserializeObject(datos);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        if (json.Message == "OK")
                        {
                            result = "OK";
                        }
                        else if (json.Message == "ErrorEnRuta")
                        {
                            result = "ErrorEnRuta";
                        }
                        else if (json.Message == "DataError")
                        {
                            result = "DataError";
                        }
                        else
                        {
                            result = "ErrorEnDB";
                        }

                    }
                    else
                    {
                        result = "ErrorEnDB";
                    }
                }
                else
                {
                    result = "ErrorEnDB";
                }

            }
            catch (TaskCanceledException ex)
            {
                result = "Se canceló una tarea.";
            }
            catch (Exception ex)
            {
                result = "Se cancelo";
            }
            return result;
        }
        public Task<string> ExecuteApiPost(string url, string controller, string method)
        {
            Task<string> result = null;
            try
            {
                dynamic json = "";

                string urlData = url + controller + method;
                HttpClient clienteHttp = new HttpClient();

                var httpResponse = clienteHttp.GetAsync(urlData);

                var datos = httpResponse.Result.Content.ReadAsStringAsync();


                result = datos;
            }
            catch (Exception)
            {
            }

            return result;
        }

        public async Task<object> ExecuteApiGetData(string url, string controller, string method)
        {
            dynamic result = null;
            try
            {
                dynamic json = "";

                string urlData = url + controller + method;
                HttpClient clienteHttp = new HttpClient();

                var httpResponse = clienteHttp.GetAsync(urlData);

                var datos = await httpResponse.Result.Content.ReadAsStringAsync();
                json = JsonConvert.DeserializeObject(datos);


                result = json.Data;
            }
            catch (Exception)
            {
            }

            return result;
        }
    }
}
