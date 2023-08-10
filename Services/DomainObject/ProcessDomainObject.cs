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
    public class ProcessDomainObject
    {
        private readonly ProcessDAO DAO;
        public ProcessDomainObject()
        {
            DAO = new ProcessDAO();
        }

        /// <summary>
        /// Servicio para guardar movimientos de empleados
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="DeliveryQuantity"></param>
        /// <param name="MonthId"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public MovementsResponse SaveMovements(Int64 EmployeeId, int DeliveryQuantity, int MonthId)
        {
            MovementsResponse result = new MovementsResponse();
            try
            {
                result = DAO.SaveMovements(EmployeeId, DeliveryQuantity, MonthId);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
