
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ITransactionService<T> : IEntityService<T>
    {
        //T BuildTransaction(T row);

        T GetByIdIncLocations(int? id);

        List<T> GetAllDeleted();

        List<T> GetAllByCustomerId(int id);

        T NegateTransaction(T row);

        T GetByDeletedId(int? id);

        ICollection<T> GetByBicycleId(int id);

        T ReturnBicycle(T row);

    }
}
