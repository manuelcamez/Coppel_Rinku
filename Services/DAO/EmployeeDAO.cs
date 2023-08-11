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

        /// <summary>
        /// Servicio para guardar un Employee
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="RolId"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public EmployeeResponse SaveEmployee(string Name, Decimal HourlyWage, Int64 RolId)
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


        /// <summary>
        /// Servicio para actualizar un empleado
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="RolId"></param>
        /// <exception cref="DataException"></exception>
        public void UpdateEmployeeById(Int64 Id, string Name, decimal HourlyWage, Int64 RolId)
        {
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.Update_Employee;
                command.CommandType = CommandType.StoredProcedure;

                var parameter0 = command.CreateParameter();
                parameter0.ParameterName = "@Id";
                parameter0.Value = Id;
                command.Parameters.Add(parameter0);

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

            }
            catch (Exception e)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Servicio para inactivar un empleado
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="DataException"></exception>
        public void InactiveEmployeeById(Int64 Id)
        {
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.Inactivate_Employee;
                command.CommandType = CommandType.StoredProcedure;

                var parameter0 = command.CreateParameter();
                parameter0.ParameterName = "@Id";
                parameter0.Value = Id;
                command.Parameters.Add(parameter0);

                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

            }
            catch (Exception e)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Servicio para Obtener datos de un empleado por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public EmployeeResponse GetEmployeeById(Int64 Id)
        {
            List<EmployeeResponse> EmployeeData = new List<EmployeeResponse>();
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.Get_EmployeeById;
                command.CommandType = CommandType.StoredProcedure;

                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "@Id";
                parameter1.Value = Id;
                command.Parameters.Add(parameter1);

                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                EmployeeData =
                ((IObjectContextAdapter)context).ObjectContext.Translate<EmployeeResponse>
                (reader).ToList();
                reader.NextResult();

            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return EmployeeData.FirstOrDefault();
        }
    }
}
