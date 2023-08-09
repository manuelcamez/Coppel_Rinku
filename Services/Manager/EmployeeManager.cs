using Contracts.Response;
using Services.DomainObject;
using System;
using System.Collections.Generic;
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
    }
}
