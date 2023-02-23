using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface IBicycleTypeService<T> : IEntityService<T>
    {
        public BicycleType GetByName(string name);
    }
}
