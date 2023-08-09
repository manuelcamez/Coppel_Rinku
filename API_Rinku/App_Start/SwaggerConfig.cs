using System.Web.Http;
using WebActivatorEx;
using API_Rinku;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace API_Rinku
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1","API_Rinku");
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
