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
    public class EmployeeManager
    {
        private readonly EmployeeDomainObject domainObject;
        public EmployeeManager()
        {
            domainObject = new EmployeeDomainObject();
        }
        public EmployeeResponse SaveEmployee(string Name, double HourlyWage, Int64 RolId)
        {
            return domainObject.SaveEmployee(Name, HourlyWage, RolId);
        }

        public void UpdateEmployeeById(Int64 Id,string Name, double HourlyWage, Int64 RolId)
        {
            try 
            {
                domainObject.UpdateEmployeeById(Id, Name, HourlyWage, RolId);
            }
            catch (Exception e)
            {
                throw new DataException();
            }

        }

        /// <summary>
        /// Servicio para Obtener datos de un empleado por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public EmployeeResponse GetEmployeeById(Int64 Id)
        {
            EmployeeResponse result = new  EmployeeResponse();
            try
            {
                result = domainObject.GetEmployeeById(Id);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
