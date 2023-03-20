using BikesTest.Exceptions;
using BikesTest.Interfaces;
using BikesTest.Models;
using BikesTest.ServiceExtentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class ReservationServices : IReservationService<Reservation>
    {
        private readonly Context _db;
        private readonly IAdminService<Admin> _aService;
        private readonly IUserService<Customer> _cService;
        private readonly ICouponService<Coupon> _coService;
        private readonly IBicycleService<Bicycle> _bService;
        private readonly ITransactionService<Transaction> _tService;
        public ReservationServices(Context db,
                                   IAdminService<Admin> aService,
                                   IUserService<Customer> cService,
                                   ICouponService<Coupon> coService,
                                   IBicycleService<Bicycle> bService,
                                   ITransactionService<Transaction> tService)
        {
            _db = db;
            _aService = aService;
            _cService = cService;
            _coService = coService;
            _bService = bService;
            _tService = tService;

        }

        public void VerifyExpiration(int hourDelay)
        {
            List<Reservation> reservations = _db.Reservations.AsNoTracking()
                                                             .Where(o => o.isDeleted == false && o.reservationDate.AddHours(hourDelay)  /* delay = 2h */ < DateTime.Now)
                                                             .Include(o => o.bicycle)
                                                             .ToList();
            if(reservations.Count != 0)
            {
                foreach(var res in reservations)
                {
                    if(res.subscription_Id == null || res.subscription_Id == 0 || res.expectedReturnDate < DateTime.Now)
                    {
                        res.isDeleted = true;
                        _bService.UpdateIsReserved(res.bicycle);
                    }
                }

                foreach (var res in reservations)
                {
                    if (reservations.Any(o => (o.bicycle_Id == res.bicycle_Id && o.bicycle != null) && o.id != res.id))
                    {
                        res.bicycle = null;
                    }
                }

                _db.UpdateRange(reservations);
                _db.SaveChanges();

                foreach (var res in reservations)
                {
                    _db.Entry(res).State = EntityState.Detached;
                }
            }
        }

        public void CancelReservation(Reservation row)
        {
            //do canceling stuff

            Coupon coupon = _coService.GetById(row.coupon_Id);
            if(coupon != null)
            {
                row.coupon = coupon;
                row.coupon.isReserved = false;
                _db.Update(row.coupon);
            }
                

            //mark the reservation as deleted but keep it for logs
            row.isDeleted = true;
            _bService.UpdateIsReserved(row.bicycle);
            
            _db.Update(row);
            _db.SaveChanges();
        }

        public void ConfirmReservation(Reservation row, int currentAdminId)
        {

            _aService.CheckSuspended(currentAdminId);
            row.customer = null;

            Transaction transaction = new Transaction
            {
                rentalDate = row.reservationDate,
                expectedReturnDate = row.expectedReturnDate,
                admin_Id = currentAdminId,
                bicycleType_Id = row.bicycle.bicycleType_Id,
                bicycle_Id = row.bicycle_Id,
                bicycle = row.bicycle,
                customer_Id = row.customer_Id,
                coupon_Id = row.coupon_Id,
                subscription_Id = row.subscription_Id
            };
           
            row.isDeleted = true;
            _bService.UpdateIsReserved(row.bicycle);
            row.bicycle = null;

            _db.Update(row);

            _tService.Create(transaction);
        }

        public Reservation Create(Reservation row)
        {
            row.id = 0;
            Bicycle bike = _bService.GetFirstAvailableByTypeId(row.bicycleType_Id, row.reservationDate, row.expectedReturnDate);
            Customer customer = _cService.GetById(row.customer_Id, true, false);
            row.customer = customer;

            this.ReservationVerifications(row, customer, bike);
            row.bicycle_Id = bike.id;

            if (row.coupon_Id == 0 || row.coupon_Id == null)
                row.coupon_Id = null;
            else
                _coService.Reserve(row, customer);

            //all checks OK!


            bike.isReserved = true;

            row.customer = null;
            row.bicycle = null;
            customer.coupons = null;
            customer.transactions = null;

            _db.Bicycles.Update(bike);
            _db.Reservations.Update(row);
            _db.SaveChanges();

            row.customer = customer;

            return row;
        }

        public void Delete(int id)
        {
            _db.Reservations.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Delete(Reservation row)
        {
            _db.Reservations.Remove(row);
            _db.SaveChanges();
        }

        public List<Reservation> GetAll()
        {
            VerifyExpiration(2);
            return _db.Reservations.AsNoTracking()
                                   .Where(o => o.isDeleted == false)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(o => o.bicycle)
                                   .ToList<Reservation>();
        }

        public Reservation GetByBicycleId(int bicycleId)
        {
            VerifyExpiration(2);
            return _db.Reservations.AsNoTracking()
                                   .Where(o => o.bicycle_Id == bicycleId && o.isDeleted == false)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(o => o.bicycle)
                                   .FirstOrDefault();
        }

        public Reservation GetById(int? id)
        {
            VerifyExpiration(2);
            return _db.Reservations.AsNoTracking()
                                   .Where(o => o.id == id && o.isDeleted == false)
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Include(o => o.bicycle)
                                   .FirstOrDefault();
        }

        public List<Reservation> GetByCustomerUserId(int id)
        {
            VerifyExpiration(2);
            return _db.Reservations.AsNoTracking()
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Where(o => o.customer.user_id == id && o.isDeleted == false)
                                   .Include(o => o.bicycle)
                                   .ToList();
        }
        public Reservation GetByUsername(string username)
        {
            VerifyExpiration(2);
            return _db.Reservations.AsNoTracking()
                                   .Include(o => o.customer).ThenInclude(m => m.user)
                                   .Where(o => o.customer.user.username == username && o.isDeleted == false)
                                   .Include(o => o.bicycle)
                                   .FirstOrDefault();
        }

        public List<Reservation> Search()
        {
            throw new NotImplementedException();
        }

        public Reservation Update(Reservation row)
        {
            VerifyExpiration(2);

            Bicycle bike = _bService.GetById(row.bicycle_Id);
            Customer customer = _cService.GetById(row.customer_Id);

            this.ReservationVerifications(row, customer, bike);
            //all checks OK!

            _db.Reservations.Update(row);
            _db.SaveChanges();

            return row;
        }
    }
}
