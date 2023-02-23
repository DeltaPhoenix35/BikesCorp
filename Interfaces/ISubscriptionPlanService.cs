using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ISubscriptionPlanService<T> : IEntityService<T>
    {
        public T Activate(T row);

        public T Disable(T row);

        public List<T> GetAll(bool isDeleted, bool isActive);

        public T GetByName(string name);

    }
}
