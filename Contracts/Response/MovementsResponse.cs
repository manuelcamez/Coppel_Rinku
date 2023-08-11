using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response
{
    public class MovementsResponse
    {
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Int64? RolId { get; set; }
        public string Rol { get; set; }
        public int MonthId { get; set;}
        public string Month { get; set; }
        public int? DeliveryQuantity { get; set; }
        
    }
}
