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
    public class CatalogueDAO
    {
        public readonly DataBaseContext context;

        public CatalogueDAO()
        {
            context = new DataBaseContext();
        }

        /// <summary>
        /// Servicio para obtener el catalago de months
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public List<MonthResponse> GetAll_Months()
        {
            List<MonthResponse> MonthsData = new List<MonthResponse>();
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.GetAll_Months;
                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                MonthsData =
                ((IObjectContextAdapter)context).ObjectContext.Translate<MonthResponse>
                (reader).ToList();
                reader.NextResult();

            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return MonthsData;
        }

        /// <summary>
        /// Servicio para obtener el catalago de roles
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public List<RolResponse> GetAll_Rol()
        {
            List<RolResponse> Data = new List<RolResponse>();
            try
            {
                var command = context.Database.Connection.CreateCommand(); command.CommandText = SP.GetAll_Rol;
                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = 32767;
                context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                Data =
                ((IObjectContextAdapter)context).ObjectContext.Translate<RolResponse>
                (reader).ToList();
                reader.NextResult();

            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return Data;
        }
    }
}
