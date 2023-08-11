using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class MovementsRequest
    {
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public int DeliveryQuantity { get; set; }
        public int MonthId { get; set; }
    }
}
