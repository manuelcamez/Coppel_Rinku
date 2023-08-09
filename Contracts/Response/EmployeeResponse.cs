using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response
{
    public class EmployeeResponse
    {
        public string Name { get; set; }
        public Decimal HourlyWage { get; set; }
        public Int64 RolId { get; set; }
    }
}
