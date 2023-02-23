using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Interfaces
{
    public interface ICouponService <T> : IEntityService<T>
    {
        public T Redeem(CouponType row, Customer customer);

        public T Reserve(Reservation reservation, Customer customer);

        public T Apply(Transaction transaction, Customer customer);

        public List<T> GetAll(bool deleted, bool used, bool expired, bool reserved);

        public List<T> GetByCustomerId(int id, bool deleted, bool used, bool expired, bool reserved);

        public List<T> GetByUserId(int id, bool deleted, bool used, bool expired, bool reserved);

        public void VerifyExpiration();
    }
}
