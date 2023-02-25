using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ISubscriptionService<T> : IEntityService<T>
    {
        //public T Buy(int id, int customerId);

        public List<T> GetAll(bool isActive, bool isDeleted);

        public List<T> GetByCustomerId(int id, bool isActive, bool isDeleted);

        public T Renew(T row);
    }
}
