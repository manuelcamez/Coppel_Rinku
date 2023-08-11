using System;

namespace WPF_Rinku.Views
{
    public class EmpleadoInfo
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyWage { get; set; }
        public Int64 RolId { get; set; }
        public static object ItemsSource { get; internal set; }

        public EmpleadoInfo(Int64 id,string name, decimal hourlyWage, Int64 rolId)
        {
            Id = id;
            Name = name;
            HourlyWage = hourlyWage;
            RolId = rolId;
        }

        public override string ToString()
        {
            return $"{Name} ({RolId}) - Salario: ${HourlyWage} por hora";
        }

        internal static void Add(EmpleadoInfo nuevoEmpleado)
        {
            throw new NotImplementedException();
        }
    }
}