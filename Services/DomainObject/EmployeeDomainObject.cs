using Contracts.Response;
using Services.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.DomainObject
{
    public class EmployeeDomainObject
    {
        private readonly EmployeeDAO DAO;
        public EmployeeDomainObject()
        {
            DAO = new EmployeeDAO();
        }

        /// <summary>
        /// Servicio para guardar un Employee
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="RolId"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public EmployeeResponse SaveEmployee(string Name, Decimal HourlyWage, Int64 RolId)
        {
            EmployeeResponse result = new EmployeeResponse();
            try
            {
                result = DAO.SaveEmployee(Name, HourlyWage, RolId);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }

        /// <summary>
        /// Servicio para actualizar un empleado
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="RolId"></param>
        /// <exception cref="DataException"></exception>
        public void UpdateEmployeeById(Int64 Id, string Name, decimal HourlyWage, Int64 RolId)
        {
            try
            {
                DAO.UpdateEmployeeById(Id,Name, HourlyWage, RolId);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Servicio para inactivar un empleado
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="DataException"></exception>
        public void InactiveEmployeeById(Int64 Id)
        {
            try
            {
                DAO.InactiveEmployeeById(Id);
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
            EmployeeResponse result = new EmployeeResponse();
            try
            {
                result = DAO.GetEmployeeById(Id);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
