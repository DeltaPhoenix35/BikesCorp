using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface IBicycleService<T> : IEntityService<T>
    {
        public List<T> GetAllAvailable();
        public T GetFirstAvailable(DateTime start, DateTime end);
        public T GetFirstAvailableByTypeId(int typeId, DateTime start, DateTime end);

    }
}
