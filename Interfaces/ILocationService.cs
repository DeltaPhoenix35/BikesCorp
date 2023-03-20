using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ILocationService
    {
        public List<Location> GetAll(int transactionId, int bicycleId);

        public Location SetActive(int bicycleId);

        public Location UpdateLastTransactionId(int bicycleId, int transactionId);

        public Location ResetActive(int bicyckeId);
    }
}
