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

        public SubscriptionService(Context db,
                                   IReservationService<Reservation> rService)
        {
            _db = db;
            _rService = rService;
        }


        public Subscription Create(Subscription row)
        {
            row.subscriptionPlan = _db.SubscriptionPlans.AsNoTracking()
                                               .Where(o => o.id == row.subscriptionPlan_Id)
                                               .Include(o => o.subscriptionDays)
                                               .FirstOrDefault();

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

            _db.Update(row);
            _db.SaveChanges();
            return row;
        }

        public void Delete(int id)
        {
            var row = _db.Subscriptions.AsNoTracking().Where(o => o.id == id).FirstOrDefault();
            _db.Remove(row);
            _db.SaveChanges();
        }

        public void Delete(Subscription row)
        {
            _db.Remove(row);
            _db.SaveChanges();
        }

        public List<Subscription> GetAll(bool isActive)
        {
            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.isActive == isActive)
                                    .ToList();
        }

        public List<Subscription> GetAll()
        {
            return _db.Subscriptions.AsNoTracking()
                                    .ToList();
        }

        public Subscription GetById(int? id)
        {
            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.id == id)
                                    .Include(o => o.subscriptionPlan).ThenInclude(t => t.subscriptionDays)
                                    .Include(o => o.customer).ThenInclude(t => t.user)
                                    .FirstOrDefault();
        }

        public List<Subscription> GetByCustomerId(int id, bool isActive)
        {
            return _db.Subscriptions.AsNoTracking()
                                    .Where(o => o.customer_Id == id && o.isActive == isActive)
                                    .Include(o => o.subscriptionPlan).ThenInclude(t => t.subscriptionDays)
                                    .Include(o => o.customer).ThenInclude(t => t.user)
                                    .ToList();
        }

        public Subscription GetByUsername(string username)
        {
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