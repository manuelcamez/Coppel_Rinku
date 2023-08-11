using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class CalculationRequest
    {
        public Int64 EmployeeId { get; set; }
        public int MonthId { get; set; }
    }
}
