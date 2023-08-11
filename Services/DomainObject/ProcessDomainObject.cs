using Contracts.Request;
using Contracts.Response;
using Services.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.DomainObject
{
    public class ProcessDomainObject
    {
        private readonly ProcessDAO DAO;
        public ProcessDomainObject()
        {
            DAO = new ProcessDAO();
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
            MovementsResponse result = new MovementsResponse();
            try
            {
                result = DAO.SaveMovements(EmployeeId, DeliveryQuantity, MonthId);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }

        /// <summary>
        /// Servicio para obtener movimientos por su Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public MovementsResponse GetMovementById(Int64 Id)
        {
            MovementsResponse result = new MovementsResponse();
            try
            {
                result = DAO.GetMovementById(Id);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }

        /// <summary>
        /// Servicio para actualizar un Movimiento
        /// </summary>
        /// <param name="movements"></param>
        /// <exception cref="DataException"></exception>
        public void UpdateMovement(MovementsRequest movements)
        {
            try
            {
                DAO.UpdateMovement(movements);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Servicio para inactivar un movements por id
        /// </summary>
        /// <param name="Id"></param>
        /// <exception cref="DataException"></exception>
        public void InactiveMovementById(Int64 Id)
        {
            try
            {
                DAO.InactiveMovementById(Id);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Servicio para obtener el salario detallado de un empleado
        /// </summary>
        /// <param name="movements"></param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        public CalculationsResponse CalculateSalaryAndCompensation(CalculationRequest movements)
        {
            CalculationsResponse result = new CalculationsResponse();
            try
            {
                result = DAO.CalculateSalaryAndCompensation(movements);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }
    }
}
