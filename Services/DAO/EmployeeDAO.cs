using Contracts.Response;
using Models.DataBaseConection;
using Services.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAO
{
    public class EmployeeDAO
    {
        public readonly DataBaseContext context;

        public EmployeeDAO()
        {
            context = new DataBaseContext();
        }
        public EmployeeResponse SaveEmployee(string Name, double HourlyWage, Int64 RolId)
        {
            List <EmployeeResponse> EmployeeData = new List <EmployeeResponse>();
            try 
            {
           

                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.Save_Employee;
                command.CommandType = CommandType.StoredProcedure; 

                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "@Name";
                parameter1.Value = Name;
                command.Parameters.Add(parameter1);

                var parameter2 = command.CreateParameter();
                parameter2.ParameterName = "@HourlyWage";
                parameter2.Value = HourlyWage;
                command.Parameters.Add(parameter2);

                var parameter3 = command.CreateParameter();
                parameter3.ParameterName = "@RolId";
                parameter3.Value = RolId;
                command.Parameters.Add(parameter3);


                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                EmployeeData =
                ((IObjectContextAdapter)context).ObjectContext.Translate<EmployeeResponse>
                (reader).ToList();
                reader.NextResult();


            }
            catch (Exception e) {
                throw new DataException();
            }
            return EmployeeData.FirstOrDefault();
        }
    }
}
