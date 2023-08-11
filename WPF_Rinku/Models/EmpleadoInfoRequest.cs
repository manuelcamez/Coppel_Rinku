using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Rinku.Models
{
    public class EmpleadoInfoRequest
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyWage { get; set; }
        public Int64 RolId { get; set; }
    }
}
