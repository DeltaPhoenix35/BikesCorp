using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class CouponServices : ICouponService<Coupon>
    {
        private readonly Context _db;
        public CouponServices(Context db)
        {
            _db = db;
        
        }

        public void VerifyExpiration()
        {
            List <Coupon> coupons = _db.Coupons.AsNoTracking()
                                        .Where(o => o.isDeleted == false &&
                                                    o.isUsed == false &&
                                                    o.isExpired == false)
                                        .ToList();
            foreach (var coupon in coupons)
            {
                if (coupon.expiringDate < DateTime.Now)
                {
                    coupon.isExpired = true;
                }
            }

            _db.UpdateRange(coupons);
            _db.SaveChanges();

            foreach (var coupon in coupons)
            {
                _db.Entry(coupon).State = EntityState.Detached;
            }
        }

        public Coupon Reserve(Reservation reservation, Customer customer)
        {
            reservation.coupon = customer.coupons.Where(o => o.id == reservation.coupon_Id).FirstOrDefault();

            if (reservation.coupon.expiringDate < reservation.reservationDate)
                throw new InvalidDateException("the coupon will be expired before the reservation");

            if (reservation.coupon.isReserved == true)
                throw new CurrentlyReservedException("the coupon is already reserved");

            if (reservation.coupon.isUsed == true)
                throw new CurrentlyUsedException("the coupon is already used");

            reservation.coupon.isReserved = true;

            //Update(row);

            return reservation.coupon;
        }

        public Coupon Apply(Transaction transaction, Customer customer)
        {
            transaction.coupon = customer.coupons.Where(o => o.id == transaction.coupon_Id).FirstOrDefault();
            transaction.coupon.isUsed = true;
            transaction.coupon.isReserved = false;

            //Update(row);

            return transaction.coupon;
        }

        public Coupon Redeem(CouponType couponType, Customer customer)
        {
            Coupon coupon = new Coupon
            {
                couponType_Id = couponType.id,
                customer_Id = customer.id,
                expiringDate = DateTime.Now.AddDays(couponType.weeksToExpire * 7),
                isDeleted = false,
                isExpired = false,
                isUsed = false,
                isReserved = false
            };

            customer.points -= couponType.pointsToRedeem;

            customer.coupons = null;

            coupon.customer = customer;
            
            Update(coupon);
            
            return coupon;
        }

        public Coupon Create(Coupon row)
        {
            _db.Add(row);
            _db.SaveChanges();
            return row;
        }

        public void Delete(int id)
        {
            Coupon coupon = GetById(id);
            coupon.isDeleted = true;
            Update(coupon);
        }

        public void Delete(Coupon row)
        {
            row.isDeleted = true;
            Update(row);
        }

        public List<Coupon> GetAll()
        {
            VerifyExpiration();

            List<Coupon> coupons = _db.Coupons.AsNoTracking()
                                      .Include(o => o.couponType)
                                      .ToList();
            return coupons;
        }

        public List<Coupon> GetAll(bool deleted, bool used, bool expired, bool reserved)
        {
            VerifyExpiration();

            List<Coupon> coupons = _db.Coupons.AsNoTracking()
                                      .Where(o => o.isDeleted == deleted && 
                                                  o.isUsed == used && 
                                                  o.isExpired == expired && 
                                                  o.isReserved == reserved)
                                      .Include(o => o.couponType)
                                      .ToList();
            return coupons;
        }

        public List<Coupon> GetByCustomerId(int id, bool deleted, bool used, bool expired, bool reserved)
        {
            VerifyExpiration();

            List<Coupon> coupons = _db.Coupons.AsNoTracking()
                                      .Where(o => o.isDeleted == deleted &&
                                                  o.isUsed == used &&
                                                  o.isExpired == expired &&
                                                  //o.isReserved == reserved &&
                                                  o.customer_Id == id)
                                      .Include(o => o.couponType)
                                      .ToList();
            return coupons;
        }

        public List<Coupon> GetByUserId(int id, bool deleted, bool used, bool expired, bool reserved)
        {
            VerifyExpiration();

            List<Coupon> coupons = _db.Coupons.AsNoTracking()
                                      .Where(o => o.isDeleted == deleted &&
                                                  o.isUsed == used &&
                                                  o.isExpired == expired &&
                                                  o.isReserved == reserved)
                                      .Include(o => o.couponType)
                                      .Include(o => o.customer).ThenInclude(t => t.user).Where(o => o.customer.user.id == id)
                                      .ToList();
            return coupons;
        }

        public Coupon GetById(int? id)
        {
            VerifyExpiration();

            Coupon coupon = _db.Coupons.AsNoTracking()
                                       .Where(o => o.id == id)
                                       .Include(o => o.couponType)
                                       .FirstOrDefault();
            return coupon;
        }

        public Coupon GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public List<Coupon> Search()
        {
            throw new NotImplementedException();
        }

        public Coupon Update(Coupon row)
        {
            _db.Update(row);
            _db.SaveChanges();
            return row;
        }
    }
}
