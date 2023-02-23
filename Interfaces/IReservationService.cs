using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface IReservationService<T> : IEntityService<T>
    {
        public T GetByBicycleId(int bicycleId);

        public void ConfirmReservation(T row, int currentAdminId);

        public void CancelReservation(T row);

        public List<T> GetByCustomerUserId(int id);

        void VerifyExpiration(int hourDelay);
    }
}
