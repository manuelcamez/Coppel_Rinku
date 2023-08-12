using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Rinku.Models
{
    public class CalculationsInfoRequest
    {
        public int Employee_Id { get; set; }
        public string EmployeeName { get; set; }
        public string RolName { get; set; }
        public decimal HourlyWage { get; set; }
        public string Mounth { get; set; }
        public int DeliveryQuantity { get; set; }
        public int TotalHourByMounth { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal AditionalBonus { get; set; }
        public decimal BonusByHoursByMonth { get; set; }
        public decimal GroceryVouchers { get; set; }
        public decimal Isr { get; set; }
        public decimal MonthlyNetSalary { get; set; }
    }
}
