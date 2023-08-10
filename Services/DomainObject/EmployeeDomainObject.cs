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
        public EmployeeResponse SaveEmployee(string Name, double HourlyWage, Int64 RolId)
        {
            return DAO.SaveEmployee(Name, HourlyWage, RolId);
        }
        public void UpdateEmployeeById(Int64 Id, string Name, double HourlyWage, Int64 RolId)
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
        public EmployeeResponse GetEmployeeById(Int64 Id)
        {
            return DAO.GetEmployeeById(Id);
        }
    }
}
