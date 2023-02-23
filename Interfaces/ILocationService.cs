using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ILocationService
    {
        public Task<List<Location>> GetAll(int transactionId, int bicycleId);
        public Task<long> GetLastTransactionId(int bicycleId);
    }
}
