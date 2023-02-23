using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface IContractService<T> : IEntityService<T>
    {
        public List<T> GetAll(bool active, bool denied);

        public List<T> GetAllByUsername(string username, bool active, bool denied);

        public List<T> GetAllByCustomerId(int id, bool active, bool denied);

        public T Cancel(T row);

        public T Confirm(T row);

        public T Deny(T row, string refusalInformation);

    }
}
