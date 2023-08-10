using Contracts.Response;
using Services.DomainObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Manager
{
    public class CatalogueManager
    {
        private readonly CatalogueDomainObject domainObject;

        public CatalogueManager()
        {
            domainObject = new CatalogueDomainObject();   
        }
        /// <summary>
        /// Servicio para obtener el catalago de months
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public List<MonthResponse> GetAll_Months()
        {
            List<MonthResponse> result = new List<MonthResponse>();
            try
            {
                result = domainObject.GetAll_Months();
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }

        /// <summary>
        /// Servicio para obtener el catalago de roles
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public List<RolResponse> GetAll_Rol()
        {
            List<RolResponse> result = new List<RolResponse>();
            try
            {
                result = domainObject.GetAll_Rol();
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
