using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WPF_Rinku.Views;

namespace WPF_Rinku.Models
{
    public class MovementInfo
    {
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public int DeliveryQuantity { get; set; }
        public int MonthId { get; set; }
        public static object ItemsSource { get; internal set; }

        public MovementInfo(Int64 id, Int64 employeeId, int deliveryQuantity, int monthId)
        {
            Id = id;
            EmployeeId = employeeId;
            DeliveryQuantity = deliveryQuantity;
            MonthId = monthId;
        }

        public override string ToString()
        {
            return $"{EmployeeId} ({DeliveryQuantity}) - DeliveryQuantity: {DeliveryQuantity} by mounth";

        }

        internal static void Add(MovementInfo newMovement)
        {
            throw new NotImplementedException();
        }
    }
}
