using Contracts.Request;
using Contracts.Response;
using Services.DomainObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Manager
{
    public class ProcessManager
    {
        private readonly ProcessDomainObject domainObject;
        public ProcessManager()
        {
            domainObject = new ProcessDomainObject();
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
                result =  domainObject.SaveMovements(EmployeeId, DeliveryQuantity, MonthId);
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
                result = domainObject.GetMovementById(Id);
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
                domainObject.UpdateMovement(movements);
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
                domainObject.InactiveMovementById(Id);
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
                result = domainObject.CalculateSalaryAndCompensation(movements);
            }
            catch (Exception e)
            {
                throw new DataException();
            }
            return result;
        }

    }
}
