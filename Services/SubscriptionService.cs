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
    public class SubscriptionService : ISubscriptionService<Subscription>
    {

        private readonly Context _db;
        private readonly IReservationService<Reservation> _rService;
        private readonly IBicycleService<Bicycle> _bService;

        public SubscriptionService(Context db,
                                   IReservationService<Reservation> rService,
                                   IBicycleService<Bicycle> bService)
        {
            _db = db;
            _rService = rService;
            _bService = bService;
        }


        public void VerifyExpiration()
        {
            List<Subscription> subscriptions = _db.Subscriptions.AsNoTracking()
                                                                 .Where(o => o.isActive == true && o.isDeleted == false && o.creationDate.AddMonths(1) < DateTime.Now)
                                                                 .ToList();
            if (subscriptions != null)
            {
                foreach (var sub in subscriptions)
                {
                    sub.isActive = false;
                }

                _db.UpdateRange(subscriptions);
                _db.SaveChanges();

                foreach (var res in subscriptions)
                {
                    _db.Entry(res).State = EntityState.Detached;
                }
            }
        }


        public Subscription Create(Subscription row)
        {
            row.subscriptionPlan = _db.SubscriptionPlans.AsNoTracking()
                                               .Where(o => o.id == row.subscriptionPlan_Id)
                                               .Include(o => o.subscriptionDays)
                                               .FirstOrDefault();

            if(row.subscriptionPlan == null)
                throw new SubscriptionPlanDoesntExistException("This Subscription Plan Doesn't Exist");
            else if(row.subscriptionPlan.isActive == false)
                throw new SubscriptionPlanIsntActive("This Subscription Plan Is NOT Active");
            else if(row.subscriptionPlan.isDeleted == true)
                throw new SubscriptionPlanIsDeleted("This Subscription Plan Is Deleted");

            row.creationDate = DateTime.Now;
            row.isActive = true;

            //create reservations accordingly
            List<DateTime> startTimes = new List<DateTime>();
            List<DateTime> endTimes = new List<DateTime>();

            this.GetSubscriptionDates(row, startTimes, endTimes);

            row.reservations = new List<Reservation>();
            for(int i = 0; i < startTimes.Count; i++)
            {
                var res = _rService.Create(new Reservation
                        {
                            bicycleType_Id = row.bicycleType_Id,
                            customer_Id = row.customer_Id,
                            reservationDate = startTimes[i],
                            expectedReturnDate = endTimes[i]
                        });
                res.customer = null;
                res.bicycle = null;

                row.reservations.Add(res);

                _db.ChangeTracker.Clear();
            }

            var customer = _db.Customers.AsNoTracking().Where(o => o.id == row.customer_Id)
                                                       .Include(o => o.subscriptions)
                                                       .FirstOrDefault();
            var sub = customer.subscriptions.Where(s => s.subscriptionPlan_Id == row.subscriptionPlan_Id).FirstOrDefault();
            
            if (sub != null)
            {
                row.id = sub.id;
                row.isActive = true;
                row.isDeleted = false;
            }

            sub = null;
            customer = null;

            _db.Update(row);
            _db.SaveChanges();
            return row;
        }

        public Subscription Renew(Subscription row)
        {
            row = _db.Subscriptions.AsNoTracking().Where(o => o.id == row.id).FirstOrDefault();

            Create(row);

            return row;
        }

        public void Delete(int id)
        {
            var row = _db.Subscriptions.AsNoTracking().Where(o => o.id == id)
                                       .Include(o => o.reservations.Where(t => t.isDeleted == false))
                                       .FirstOrDefault();

            if (row.isActive)
            {
                for(int i = 0; i < row.reservations.Count; i++)
                {
                    if(i == 0 || row.reservations.Any(r => r.bicycle_Id == row.reservations[i].bicycle_Id && 
                                                           r.id != row.reservations[i].id &&
                                                           r.isDeleted == false) == false)
                        row.reservations[i].bicycle = _db.Bicycles.AsNoTracking().Where(o => o.id == row.reservations[i].bicycle_Id)
                                                                  .Include(t => t.reservations.Where(r => r.isDeleted == false && r.subscription_Id == row.id))
                                                                  .FirstOrDefault();

                    else
                        row.reservations[i].bicycle = row.reservations.Where(r => r.bicycle_Id == row.reservations[i].bicycle_Id)
                                                                      .FirstOrDefault().bicycle;
                    
                    row.reservations[i].bicycle.reservations.Where(r => r.id == row.reservations[i].id).FirstOrDefault().isDeleted = true;

                    _bService.UpdateIsReserved(row.reservations[i].bicycle);

                    _db.Update(row.reservations[i].bicycle);
                    
                }                
            }

            row.isDeleted = true; row.isActive = false;

            row.reservations = null;
            
            _db.Update(row);
            _db.SaveChanges();
        }

        public void Delete(Subscription row)
        {
            _db.Remove(row);
            _db.SaveChanges();
        }

        public List<Subscription> GetAll(bool isActive, bool isDeleted)
        {
            VerifyExpiration();

            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.isActive == isActive && o.isDeleted == isDeleted)
                                    .ToList();
        }

        public List<Subscription> GetAll()
        {
            VerifyExpiration();

            return _db.Subscriptions.AsNoTracking()
                                    .ToList();
        }

        public Subscription GetById(int? id)
        {
            VerifyExpiration();

            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.id == id)
                                    .Include(o => o.subscriptionPlan).ThenInclude(t => t.subscriptionDays)
                                    .Include(o => o.customer).ThenInclude(t => t.user)
                                    .FirstOrDefault();
        }

        public List<Subscription> GetByCustomerId(int id, bool isActive, bool isDeleted)
        {
            VerifyExpiration();

            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.customer_Id == id && o.isActive == isActive && o.isDeleted == isDeleted)
                                    .Include(o => o.subscriptionPlan).ThenInclude(t => t.subscriptionDays)
                                    .Include(o => o.customer).ThenInclude(t => t.user)
                                    .ToList();
        }

        public Subscription GetByUsername(string username)
        {
            VerifyExpiration();

            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.customer.user.username == username)
                                    .Include(o => o.subscriptionPlan).ThenInclude(t => t.subscriptionDays)
                                    .Include(o => o.customer).ThenInclude(t => t.user)
                                    .FirstOrDefault();
        }

        public List<Subscription> Search()
        {
            throw new NotImplementedException();
        }

        public Subscription Update(Subscription row)
        {
            _db.Update(row);
            _db.SaveChanges();
            return row;
        }
    }
}