using BikesTest.Interfaces;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService<SubscriptionPlan>
    {

        private readonly Context _db;

        public SubscriptionPlanService(Context db)
        {
            _db = db;
        }


        public SubscriptionPlan Create(SubscriptionPlan row)
        {

            for(int i = 0; i < 7; i++)
            {
                if (row.subscriptionDaysBool[i] == true)
                    row.subscriptionDays[i].day = (DayOfWeek)Enum.GetValues(typeof(DayOfWeek)).GetValue(i);
                else
                    row.subscriptionDays[i] = null;
            }

            int j = 7;
            for (int i = 0; i < j; i++)
            {
                if (row.subscriptionDays[i] == null)
                {
                    row.subscriptionDays.Remove(row.subscriptionDays[i]);
                    i--; j--;
                } 
            }

            _db.Add(row);
            _db.SaveChanges();

            return row;
        }

        public SubscriptionPlan Activate(SubscriptionPlan row)
        {
            row.isActive = true;
            _db.Update(row);
            _db.SaveChanges();

            return row;
        }

        public SubscriptionPlan Disable(SubscriptionPlan row)
        {
            row.isActive = false;
            _db.Update(row);
            _db.SaveChanges();

            return row;
        }

        public void Delete(int id)
        {
            var row = _db.SubscriptionPlans.AsNoTracking().Where(o => o.id == id).FirstOrDefault();
            row.isDeleted = true;
            _db.Update(row);
            _db.SaveChanges();
        }

        public void Delete(SubscriptionPlan row)
        {
            row.isDeleted = true;
            _db.Update(row);
            _db.SaveChanges();
        }

        public List<SubscriptionPlan> GetAll()
        {
            return _db.SubscriptionPlans.AsNoTracking()
                                    .Include(o => o.subscriptionDays)
                                    .ToList();
        }

        public List<SubscriptionPlan> GetAll(bool isDeleted, bool isActive)
        {
            return _db.SubscriptionPlans.AsNoTracking()
                                    .Where(o => o.isActive == isActive && o.isDeleted == isDeleted)
                                    .Include(o => o.subscriptionDays)
                                    .ToList();
        }

        public SubscriptionPlan GetById(int? id)
        {
            return _db.SubscriptionPlans.AsNoTracking()
                                    .Where(o => o.id == id)
                                    .Include(o => o.subscriptionDays)
                                    .FirstOrDefault();
        }

        public SubscriptionPlan GetByName(string name)
        {
            return _db.SubscriptionPlans.AsNoTracking()
                                    .Where(o => o.name == name)
                                    .Include(o => o.subscriptionDays)
                                    .FirstOrDefault();
        }

        public List<SubscriptionPlan> Search()
        {
            throw new NotImplementedException();
        }

        public SubscriptionPlan Update(SubscriptionPlan row)
        {
            _db.Update(row);
            _db.SaveChanges();
            return row;
        }

        public SubscriptionPlan GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
