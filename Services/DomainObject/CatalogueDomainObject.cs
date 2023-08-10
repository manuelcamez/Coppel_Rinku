using Contracts.Response;
using Services.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainObject
{
    public class CatalogueDomainObject
    {
        private readonly CatalogueDAO DAO;

        public CatalogueDomainObject()
        {
            DAO = new CatalogueDAO();
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
                result = DAO.GetAll_Months();
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
                result = DAO.GetAll_Rol();
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
