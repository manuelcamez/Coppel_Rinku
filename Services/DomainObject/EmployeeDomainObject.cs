using Contracts.Response;
using Services.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
