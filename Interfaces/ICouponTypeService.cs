using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ICouponTypeService <T> : IEntityService<T>
    {
        public List<T> GetAll(bool deleted);

        public T GetById(int id, bool deleted);
    }
}
