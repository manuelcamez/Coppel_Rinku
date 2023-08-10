using Contracts.Response;
using Models.DataBaseConection;
using Services.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
namespace Services.DAO
{
    public class ProcessDAO
    {
        public readonly DataBaseContext context;

        public ProcessDAO()
        {
            context = new DataBaseContext();
        }

        /// <summary>
        /// Servicio para guardar movimientos de empleados
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="DeliveryQuantity"></param>
        /// <param name="MonthId"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public MovementsResponse SaveMovements(Int64 EmployeeId, int DeliveryQuantity, int MonthId)
        {
            List<MovementsResponse> MovementsData = new List<MovementsResponse>();
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.Save_Movement;
                command.CommandType = CommandType.StoredProcedure;

                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "@EmployeeId";
                parameter1.Value = EmployeeId;
                command.Parameters.Add(parameter1);

                var parameter2 = command.CreateParameter();
                parameter2.ParameterName = "@DeliveryQuantity";
                parameter2.Value = DeliveryQuantity;
                command.Parameters.Add(parameter2);

                var parameter3 = command.CreateParameter();
                parameter3.ParameterName = "@MonthId";
                parameter3.Value = MonthId;
                command.Parameters.Add(parameter3);

                var parameter4 = command.CreateParameter();
                parameter4.ParameterName = "@CreationUser";
                parameter4.Value = "manuel.camez";
                command.Parameters.Add(parameter4);


                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                MovementsData =
                ((IObjectContextAdapter)context).ObjectContext.Translate<MovementsResponse>
                (reader).ToList();
                reader.NextResult();

            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return MovementsData.FirstOrDefault();
        }
    }
}
